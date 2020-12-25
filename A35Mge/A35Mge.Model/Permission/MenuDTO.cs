using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.Permission
{
    public class MenuDTO
    {
        public string id { get; set; }

        public string path { get; set; }
        public string Description { get; set; }

        public string name { get; set; }

        public string component { get; set; }

        public string redirect { get; set; }
        public MetaModel meta { get; set; } = new MetaModel();
        /// <summary>
        /// 是按钮还是菜单 默认是菜单 true为菜单 false 为按钮
        /// </summary>
        public bool IsBtn { get; set; }
        /// <summary>
        /// 父ID 根目录默认为0
        /// </summary>
        public string parentId { get; set; }

    }

    public class MetaModel
    {
        public string title { get; set; }

        public bool show { get; set; }

        public bool hideChildren { get; set; }

        public string icon { get; set; }

        public string target { get; set; }
    }
}
