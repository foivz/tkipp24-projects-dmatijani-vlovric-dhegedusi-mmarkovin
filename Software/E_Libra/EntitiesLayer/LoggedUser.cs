using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public enum Role
    {
        Admin,
        Member,
        Employee
    }
    public static class LoggedUser
    {
        public static string Username { get; set; }
        public static Role? UserType { get; set; }
        public static int LibraryId { get; set; }
    }
}
