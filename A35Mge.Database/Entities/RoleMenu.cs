using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Entities
{
    /// <summary>
    /// 菜单角色关系表
    /// </summary>
    public class RoleMenu
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string MenuId { get; set; }

        public Menu Menu { get; set; }

        public Role Role { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
