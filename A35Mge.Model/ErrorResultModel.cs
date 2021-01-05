using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model
{
    public class ErrorResultModel
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public object Data { get; set; }
    }
}
