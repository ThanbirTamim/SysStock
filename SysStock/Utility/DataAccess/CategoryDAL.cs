using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility.DataAccess
{
    public class CategoryDAL : DatabaseContext
    {
        public bool Add(Category category)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT INTO Categories (Name, Description, IsActive) VALUES (@Name, @Description, @IsActive)", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@Description", (object)category.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Category category)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE Categories SET Name = @Name, Description = @Description, IsActive = @IsActive WHERE CategoryId = @CategoryId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                    cmd.Parameters.AddWithValue("@Name", category.Name);
                    cmd.Parameters.AddWithValue("@Description", (object)category.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", category.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int categoryId)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM Categories WHERE CategoryId = @CategoryId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Category GetById(int categoryId)
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Categories WHERE CategoryId = @CategoryId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Category
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
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

        public List<Category> GetAll()
        {
            try
            {
                var categories = new List<Category>();
                using (var cmd = new SqlCommand("SELECT * FROM Categories", GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new Category
                            {
                                CategoryId = Convert.ToInt32(reader["CategoryId"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
                return categories;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
