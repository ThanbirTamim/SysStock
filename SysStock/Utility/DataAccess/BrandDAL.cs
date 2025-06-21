using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility.DataAccess
{
    public class BrandDAL : DatabaseContext
    {
        public bool Add(Brand brand)
        {
            try
            {
                using (var cmd = new SqlCommand("INSERT INTO Brands (Name, Description, IsActive) VALUES (@Name, @Description, @IsActive)", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Name", brand.Name);
                    cmd.Parameters.AddWithValue("@Description", (object)brand.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(Brand brand)
        {
            try
            {
                using (var cmd = new SqlCommand("UPDATE Brands SET Name = @Name, Description = @Description, IsActive = @IsActive WHERE BrandId = @BrandId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@BrandId", brand.BrandId);
                    cmd.Parameters.AddWithValue("@Name", brand.Name);
                    cmd.Parameters.AddWithValue("@Description", (object)brand.Description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int brandId)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM Brands WHERE BrandId = @BrandId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@BrandId", brandId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Brand GetById(int brandId)
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Brands WHERE BrandId = @BrandId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@BrandId", brandId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Brand
                            {
                                BrandId = Convert.ToInt32(reader["BrandId"]),
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

        public List<Brand> GetAll()
        {
            try
            {
                var brands = new List<Brand>();
                using (var cmd = new SqlCommand("SELECT * FROM Brands", GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            brands.Add(new Brand
                            {
                                BrandId = Convert.ToInt32(reader["BrandId"]),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"])
                            });
                        }
                    }
                }
                return brands;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
