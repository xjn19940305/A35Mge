using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Entities
{
    public class User : EntityBase
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string Account { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string NickName { get; set; }

        public string Country { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
