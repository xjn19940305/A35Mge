using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model
{
    public class ResultDTO
    {
        public string Message { get; set; }

        public bool IsError { get; set; } = false;

        public int StatusCode { get; set; }

        public dynamic Data { get; set; }
    }

    //public class ResultDTO<T> : ResultDTO
    //{
    //    public dynamic Data { get; set; }
    //}
}
