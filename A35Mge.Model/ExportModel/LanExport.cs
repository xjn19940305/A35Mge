using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.ExportModel
{
    public class LanExport
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
        public int LanguageTypeId { get; set; }
        /// <summary>
        /// 语言代码例如 zh-cn en-us
        /// </summary>
        public string LanguageCode { get; set; }
        /// <summary>
        /// 语言描述例如 中文 英文
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
