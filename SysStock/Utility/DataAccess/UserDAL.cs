using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility.DataAccess
{
    public class UserDAL : DatabaseContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool Add(User user)
        {
            try
            {
                using (var cmd = new SqlCommand(@"INSERT INTO Users (Username, Password, FullName, Email, 
                Role, IsActive) VALUES (@Username, @Password, @FullName, @Email, @Role, @IsActive)",
                    GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FullName", (object)user.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role", (object)user.Role ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                Console.WriteLine();
                throw;
            }
        }

        public bool Update(User user)
        {
            try
            {
                using (var cmd = new SqlCommand(@"UPDATE Users SET Username = @Username, 
                Password = @Password, FullName = @FullName, Email = @Email, 
                Role = @Role, IsActive = @IsActive WHERE UserId = @UserId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@UserId", user.UserId);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@FullName", (object)user.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Role", (object)user.Role ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", user.IsActive);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(int userId)
        {
            try
            {
                using (var cmd = new SqlCommand("DELETE FROM Users WHERE UserId = @UserId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User GetById(int userId)
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Users WHERE UserId = @UserId", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Role = reader["Role"]?.ToString(),
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

        public List<User> GetAll()
        {
            try
            {
                var users = new List<User>();
                using (var cmd = new SqlCommand("SELECT * FROM Users", GetConnection()))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Role = reader["Role"]?.ToString(),
                                IsActive = Convert.ToBoolean(reader["IsActive"]),
                                CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                            });
                        }
                    }
                }
                return users;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Authenticate(string username, string password)
        {
            try
            {
                using (var cmd = new SqlCommand("SELECT * FROM Users WHERE Username = @Username AND Password = @Password AND IsActive = 1", GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = Convert.ToInt32(reader["UserId"]),
                                Username = reader["Username"].ToString(),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Role = reader["Role"]?.ToString(),
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
    }
}
