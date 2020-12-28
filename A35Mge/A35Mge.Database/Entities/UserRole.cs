using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Entities
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int RoleId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        public User Users { get; set; }
        public Role Role { get; set; }
    }
}
