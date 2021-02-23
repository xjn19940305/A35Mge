using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.LanguageDTO
{
    public class LanDTO
    {
        public int TranslateId { get; set; }

        public int LanguageTypeId { get; set; }
        public string TranslateCode { get; set; }

        public string TranslateContent { get; set; }

        public LanguageTypeDTO LanguageTypeDTO { get; set; }
    }
}
