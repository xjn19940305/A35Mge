using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.CommonList
{
    public class DicListDTO
    {
        public int Id { get; set; }
        public int DicTypeId { get; set; }

        public int Sort { get; set; }
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
    }
    public class DicListResponseDTO : DicListDTO
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteDate { get; set; }
        /// <summary>
        /// 是否软删除 false 未删除 true已删除
        /// </summary>
        public bool IsDelete { get; set; }
        /// <summary>
        /// 防止并发冲突锁
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        public DicTypeDTO DicType { get; set; }
    }
}
