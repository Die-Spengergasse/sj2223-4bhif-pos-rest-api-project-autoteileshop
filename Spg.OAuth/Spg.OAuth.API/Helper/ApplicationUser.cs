using System;

namespace Spg.OAuth.API.Helper
{
    public enum Roles
    { User, Admin, Salesman }
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public Guid guid { get; set; }

    }
}
