﻿using A35Mge.Model.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.CommonList
{
    public class DicListRequest : PagingModel
    {
        public int DicTypeId { get; set; }
        public string Name { get; set; }
    }

}
