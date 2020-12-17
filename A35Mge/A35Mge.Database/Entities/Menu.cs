using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Database.Entities
{
    public class Menu : EntityBase
    {
        public string MenuId { get; set; }

        /// <summary>
        /// 路由名称
        /// </summary>
        public string name { get; set; }

        public string Path { get; set; }
        /// <summary>
        /// url的打开方式 _blank则是打开新窗口主要是用于外部链接
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 缓存该路由 (开启 multi-tab 是默认值为 true)
        /// </summary>
        public bool keepAlive { get; set; }

        public string Component { get; set; }

        public string Icon { get; set; }
        /// <summary>
        /// 菜单排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 重定向
        /// </summary>
        public string redirect { get; set; }
        /// <summary>
        /// 是否隐藏菜单 false 隐藏 true 不隐藏
        /// </summary>
        public bool Show { get; set; }
        /// <summary>
        /// 是否隐藏子菜单 true隐藏 false 不隐藏
        /// </summary>
        public bool hideChildren { get; set; }
        /// <summary>
        /// 是按钮还是菜单 默认是菜单 true为按钮 false 为菜单
        /// </summary>
        public bool IsBtn { get; set; }
         /// <summary>
        /// 父ID 根目录默认为0
        /// </summary>
        public string ParentId { get; set; }
    }
}
