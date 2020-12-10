using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace A35Mge.Database.Entities
{
    /// <summary>
    /// 基础语言翻译表
    /// </summary>
    [Table("Translate")]
    public class Translate : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int TranslateId { get; set; }

        public int LanguageTypeId { get; set; }
        /// <summary>
        /// 翻译的Code
        /// </summary>
        public string TranslateCode { get; set; }
        /// <summary>
        /// 翻译内容
        /// </summary>
        public string TranslateContent { get; set; }
        [JsonIgnore]
        public LanguageType LanguageType { get; set; }
    }
}
