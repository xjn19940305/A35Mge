using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Entities
{
    public class Role : EntityBase
    {
        public int Id { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        public int Sort { get; set; }

        public ICollection<RoleMenu> roleMenus { get; set; } = new HashSet<RoleMenu>();
        public ICollection<UserRole> userRoles { get; set; } = new HashSet<UserRole>();
    }
}
