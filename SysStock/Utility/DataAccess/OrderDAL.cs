using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using SysStock.Utility.Models;

namespace SysStock.Utility.DataAccess
{
    public class OrderDAL : DatabaseContext
    {
        public void CreateNewOrder(List<OrderItem> items, decimal vatPercentage, string paymentMethod, int currentUserId)
        {
            try
            {
                var order = new Order
                {
                    UserId = currentUserId, // Get from your login system
                    OrderDate = DateTime.Now,
                    VATPercentage = vatPercentage,
                    PaymentMethod = paymentMethod,
                    OrderItems = items
                };

                // Calculate totals
                order.SubTotal = items.Sum(i => i.LineTotal);
                order.VATAmount = order.SubTotal * (vatPercentage / 100);
                order.TotalAmount = order.SubTotal + order.VATAmount - order.GrossDiscount;

                using (var orderDal = new OrderDAL())
                {
                    int orderId = orderDal.CreateOrder(order);
                    MessageBox.Show($"Order #{orderId} created successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating order: " + ex.Message);
            }
        }

        // Example of getting daily sales
        public decimal GetTodaysSales()
        {
            try
            {
                using (var orderDal = new OrderDAL())
                {
                    return orderDal.GetDailySales(DateTime.Today);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting sales data: " + ex.Message);
                return 0;
            }
        }

        // Example of voiding an order
        public void VoidOrder(int orderId, string reason)
        {
            try
            {
                using (var orderDal = new OrderDAL())
                {
                    orderDal.VoidingOrder(orderId, reason);
                    MessageBox.Show("Order voided successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error voiding order: " + ex.Message);
            }
        }

        public int CreateOrder(Order order)
        {
            using (var transaction = GetConnection().BeginTransaction())
            {
                try
                {
                    int orderId;
                    // Insert the order
                    using (var cmd = new SqlCommand(@"
                    INSERT INTO Orders (UserId, OrderDate, SubTotal, GrossDiscount, 
                    VATPercentage, VATAmount, TotalAmount, AmountPaid, ChangeGiven, 
                    PaymentMethod, Remarks)
                    VALUES (@UserId, @OrderDate, @SubTotal, @GrossDiscount, 
                    @VATPercentage, @VATAmount, @TotalAmount, @AmountPaid, @ChangeGiven, 
                    @PaymentMethod, @Remarks);
                    SELECT SCOPE_IDENTITY();", GetConnection(), transaction))
                    {
                        cmd.Parameters.AddWithValue("@UserId", (object)order.UserId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                        cmd.Parameters.AddWithValue("@SubTotal", order.SubTotal);
                        cmd.Parameters.AddWithValue("@GrossDiscount", order.GrossDiscount);
                        cmd.Parameters.AddWithValue("@VATPercentage", order.VATPercentage);
                        cmd.Parameters.AddWithValue("@VATAmount", order.VATAmount);
                        cmd.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);
                        cmd.Parameters.AddWithValue("@AmountPaid", order.AmountPaid);
                        cmd.Parameters.AddWithValue("@ChangeGiven", order.ChangeGiven);
                        cmd.Parameters.AddWithValue("@PaymentMethod", (object)order.PaymentMethod ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Remarks", (object)order.Remarks ?? DBNull.Value);

                        orderId = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Insert order items
                    foreach (var item in order.OrderItems)
                    {
                        using (var cmd = new SqlCommand(@"
                        INSERT INTO OrderItems (OrderId, ProductId, Quantity, UnitPrice, 
                        DiscountPercent, DiscountAmount, LineTotal)
                        VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice, 
                        @DiscountPercent, @DiscountAmount, @LineTotal)", GetConnection(), transaction))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmd.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                            cmd.Parameters.AddWithValue("@DiscountPercent", item.DiscountPercent);
                            cmd.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                            cmd.Parameters.AddWithValue("@LineTotal", item.LineTotal);

                            cmd.ExecuteNonQuery();
                        }

                        // Update product stock
                        using (var cmd = new SqlCommand(@"
                        UPDATE Products 
                        SET QuantityInStock = QuantityInStock - @Quantity 
                        WHERE ProductId = @ProductId", GetConnection(), transaction))
                        {
                            cmd.Parameters.AddWithValue("@ProductId", item.ProductId);
                            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    return orderId;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Order GetOrderById(int orderId)
        {
            try
            {
                Order order = null;
                using (var cmd = new SqlCommand(@"
                SELECT o.*, u.Username 
                FROM Orders o 
                LEFT JOIN Users u ON o.UserId = u.UserId 
                WHERE o.OrderId = @OrderId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            order = new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : -1,
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                                GrossDiscount = Convert.ToDecimal(reader["GrossDiscount"]),
                                VATPercentage = Convert.ToDecimal(reader["VATPercentage"]),
                                VATAmount = Convert.ToDecimal(reader["VATAmount"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                                ChangeGiven = Convert.ToDecimal(reader["ChangeGiven"]),
                                PaymentMethod = reader["PaymentMethod"]?.ToString(),
                                Remarks = reader["Remarks"]?.ToString()
                            };
                        }
                    }
                }

                if (order != null)
                {
                    // Get order items
                    using (var cmd = new SqlCommand(@"
                    SELECT oi.*, p.Name as ProductName 
                    FROM OrderItems oi 
                    INNER JOIN Products p ON oi.ProductId = p.ProductId 
                    WHERE oi.OrderId = @OrderId", GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.OrderItems.Add(new OrderItem
                                {
                                    OrderItemId = Convert.ToInt32(reader["OrderItemId"]),
                                    OrderId = Convert.ToInt32(reader["OrderId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                    LineTotal = Convert.ToDecimal(reader["LineTotal"])
                                });
                            }
                        }
                    }
                }

                return order;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var orders = new List<Order>();
                using (var cmd = new SqlCommand(@"
                SELECT o.*, u.Username 
                FROM Orders o 
                LEFT JOIN Users u ON o.UserId = u.UserId 
                WHERE o.OrderDate BETWEEN @StartDate AND @EndDate 
                ORDER BY o.OrderDate DESC", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orders.Add(new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : -1,
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                                GrossDiscount = Convert.ToDecimal(reader["GrossDiscount"]),
                                VATPercentage = Convert.ToDecimal(reader["VATPercentage"]),
                                VATAmount = Convert.ToDecimal(reader["VATAmount"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                                ChangeGiven = Convert.ToDecimal(reader["ChangeGiven"]),
                                PaymentMethod = reader["PaymentMethod"]?.ToString(),
                                Remarks = reader["Remarks"]?.ToString()
                            });
                        }
                    }
                }

                // Get order items for each order
                foreach (var order in orders)
                {
                    using (var cmd = new SqlCommand(@"
                    SELECT oi.*, p.Name as ProductName 
                    FROM OrderItems oi 
                    INNER JOIN Products p ON oi.ProductId = p.ProductId 
                    WHERE oi.OrderId = @OrderId", GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.OrderItems.Add(new OrderItem
                                {
                                    OrderItemId = Convert.ToInt32(reader["OrderItemId"]),
                                    OrderId = Convert.ToInt32(reader["OrderId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                    LineTotal = Convert.ToDecimal(reader["LineTotal"])
                                });
                            }
                        }
                    }
                }

                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetDailySales(DateTime date)
        {
            try
            {
                using (var cmd = new SqlCommand(@"
                SELECT COALESCE(SUM(TotalAmount), 0) 
                FROM Orders 
                WHERE CAST(OrderDate AS DATE) = CAST(@Date AS DATE)", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Date", date);
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void VoidingOrder(int orderId, string remarks)
        {
            using (var transaction = GetConnection().BeginTransaction())
            {
                try
                {
                    // Get order items to restore stock
                    using (var cmd = new SqlCommand("SELECT ProductId, Quantity FROM OrderItems WHERE OrderId = @OrderId",
                        GetConnection(), transaction))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int productId = Convert.ToInt32(reader["ProductId"]);
                                int quantity = Convert.ToInt32(reader["Quantity"]);

                                // Restore product stock
                                using (var updateCmd = new SqlCommand(@"
                                UPDATE Products 
                                SET QuantityInStock = QuantityInStock + @Quantity 
                                WHERE ProductId = @ProductId", GetConnection(), transaction))
                                {
                                    updateCmd.Parameters.AddWithValue("@ProductId", productId);
                                    updateCmd.Parameters.AddWithValue("@Quantity", quantity);
                                    updateCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }

                    // Update order remarks and set total amount to 0
                    using (var cmd = new SqlCommand(@"
                    UPDATE Orders 
                    SET Remarks = @Remarks, TotalAmount = 0, 
                        SubTotal = 0, VATAmount = 0, GrossDiscount = 0 
                    WHERE OrderId = @OrderId", GetConnection(), transaction))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", orderId);
                        cmd.Parameters.AddWithValue("@Remarks", "VOID: " + remarks);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Order> GetAll()
        {
            try
            {
                var orders = new List<Order>();
                using (var cmd = new SqlCommand(@"
                        SELECT o.*, u.Username
                        FROM Orders o
                        LEFT JOIN Users u ON o.UserId = u.UserId
                        ORDER BY o.OrderDate DESC", GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : -1,
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                                GrossDiscount = Convert.ToDecimal(reader["GrossDiscount"]),
                                VATPercentage = Convert.ToDecimal(reader["VATPercentage"]),
                                VATAmount = Convert.ToDecimal(reader["VATAmount"]),
                                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                                AmountPaid = Convert.ToDecimal(reader["AmountPaid"]),
                                ChangeGiven = Convert.ToDecimal(reader["ChangeGiven"]),
                                PaymentMethod = reader["PaymentMethod"]?.ToString(),
                                Remarks = reader["Remarks"]?.ToString()
                            };
                            orders.Add(order);
                        }
                    }
                }

                // Get order items for each order
                foreach (var order in orders)
                {
                    using (var cmd = new SqlCommand(@"
                        SELECT oi.*, p.Name as ProductName
                        FROM OrderItems oi
                        INNER JOIN Products p ON oi.ProductId = p.ProductId
                        WHERE oi.OrderId = @OrderId", GetConnection()))
                    {
                        cmd.Parameters.AddWithValue("@OrderId", order.OrderId);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                order.OrderItems.Add(new OrderItem
                                {
                                    OrderItemId = Convert.ToInt32(reader["OrderItemId"]),
                                    OrderId = Convert.ToInt32(reader["OrderId"]),
                                    ProductId = Convert.ToInt32(reader["ProductId"]),
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                    DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
                                    DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                                    LineTotal = Convert.ToDecimal(reader["LineTotal"])
                                });
                            }
                        }
                    }
                }

                return orders;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
