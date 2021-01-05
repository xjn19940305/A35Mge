using A35Mge.Model.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.LanguageDTO
{
    public class TranslateRequest : PagingModel
    {
        public int LanTypeId { get; set; }

        public string Code { get; set; }

        public string Value { get; set; }
    }
}
