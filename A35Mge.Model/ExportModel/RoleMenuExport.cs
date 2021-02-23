using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.ExportModel
{
    public class RoleMenuExport
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public string MenuId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
    }
}
