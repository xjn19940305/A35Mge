using A35Mge.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Business
{
    public class DicType : EntityBase
    {
        public int Id { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }

        public ICollection<DicList> DicList { get; set; } = new HashSet<DicList>();
    }
}
