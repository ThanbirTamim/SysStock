using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility.DataAccess
{
    public class ProductDAL : DatabaseContext
    {
        public bool Add(Product product)
        {
            try
            {
                using (var cmd = new SqlCommand(@"INSERT INTO Products (Name, CategoryId, BrandId, UnitPrice, 
                DiscountPercent, QuantityInStock, Description, IsActive) 
                VALUES (@Name, @CategoryId, @BrandId, @UnitPrice, @DiscountPercent, 
                @QuantityInStock, @Description, @IsActive)", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@CategoryId", (object)product.CategoryId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BrandId", (object)product.BrandId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                    cmd.Parameters.AddWithValue("@DiscountPercent", product.DiscountPercent);
                    cmd.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    cmd.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Product product)
        {
            try
            {
                using (var cmd = new SqlCommand(@"UPDATE Products SET Name = @Name, CategoryId = @CategoryId, 
                BrandId = @BrandId, UnitPrice = @UnitPrice, DiscountPercent = @DiscountPercent, 
                QuantityInStock = @QuantityInStock, Description = @Description, IsActive = @IsActive 
                WHERE ProductId = @ProductId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@CategoryId", (object)product.CategoryId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BrandId", (object)product.BrandId ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
                    cmd.Parameters.AddWithValue("@DiscountPercent", product.DiscountPercent);
                    cmd.Parameters.AddWithValue("@QuantityInStock", product.QuantityInStock);
                    cmd.Parameters.AddWithValue("@Description", (object)product.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", product.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int productId)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM Products WHERE ProductId = @ProductId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetById(int productId)
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Products WHERE ProductId = @ProductId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Name = reader["Name"].ToString(),
                                CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"]) : -1,
                                BrandId = reader["BrandId"] != DBNull.Value ? Convert.ToInt32(reader["BrandId"]) : -1,
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
                                QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                                Description = reader["Description"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Product> GetAll()
        {
            try
            {
                var products = new List<Product>();
                using (var cmd = new SqlCommand(@"SELECT p.*, b.Name as BrandName, c.Name as CategoryName 
                FROM Products p 
                LEFT JOIN Brands b ON p.BrandId = b.BrandId 
                LEFT JOIN Categories c ON p.CategoryId = c.CategoryId", GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                Name = reader["Name"].ToString(),
                                CategoryId = reader["CategoryId"] != DBNull.Value ? Convert.ToInt32(reader["CategoryId"]) : -1,
                                BrandId = reader["BrandId"] != DBNull.Value ? Convert.ToInt32(reader["BrandId"]) : -1,
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                DiscountPercent = Convert.ToDecimal(reader["DiscountPercent"]),
                                QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]),
                                Description = reader["Description"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            });
                        }
                    }
                }
                return products;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
