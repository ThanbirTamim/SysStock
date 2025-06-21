using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysStock.Utility.Models;

namespace SysStock.Utility
{
    public static class CurrentUserSession
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static string Role { get; set; }
        public static DateTime LoginTime { get; set; }

        public static void SetCurrentUser(User user)
        {
            UserId = user.UserId;
            Username = user.Username;
            FullName = user.FullName;
            Role = user.Role;
            LoginTime = DateTime.UtcNow;
        }

        public static void Clear()
        {
            UserId = 0;
            Username = null;
            FullName = null;
            Role = null;
            LoginTime = DateTime.MinValue;
        }
    }
}
