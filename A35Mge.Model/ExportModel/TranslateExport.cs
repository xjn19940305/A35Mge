using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.ExportModel
{
    public class TranslateExport
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
    }
}
