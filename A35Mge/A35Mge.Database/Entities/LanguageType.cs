using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace A35Mge.Database.Entities
{
    /// <summary>
    /// 语言种类表 包含 中文英文等等
    /// </summary>
    [Table("LanguageType")]
    public class LanguageType : EntityBase
    {
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

        public ICollection<Translate> TranslateList { get; set; } = new HashSet<Translate>();
    }
}
