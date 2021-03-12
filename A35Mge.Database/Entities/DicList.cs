using A35Mge.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Business
{
    /// <summary>
    /// 字典类型用于不同的场合
    /// </summary>
    public class DicList : EntityBase
    {
        public int Id { get; set; }

        public int DicTypeId { get; set; }

        public string Name { get; set; }

        public int Sort { get; set; }
        public DicType DicType { get; set; }
    }
}
