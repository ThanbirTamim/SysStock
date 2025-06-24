using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility.DataAccess
{
    public class OrderItemDAL : DatabaseContext
    {
        public List<OrderItem> GetAll()
        {
            var items = new List<OrderItem>();
            try
            {
                using (var cmd = new SqlCommand(@"
                SELECT oi.*, p.Name AS ProductName
                FROM OrderItems oi
                INNER JOIN Products p ON oi.ProductId = p.ProductId
                ORDER BY oi.OrderItemId DESC", GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new OrderItem
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
            catch (Exception ex)
            {
                // You may want to log this error
                throw new Exception("Error loading order items: " + ex.Message, ex);
            }
            return items;
        }

        // Optionally, add other methods like GetByOrderId(int orderId), etc.
    }
}
