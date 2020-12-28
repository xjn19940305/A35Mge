using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.Permission
{
    public class MenuRequestDTO
    {
        public string MenuId { get; set; }
        public string name { get; set; }

        public string Path { get; set; }

        public string Component { get; set; }

        public string Description { get; set; }

        public int Sort { get; set; }

        public string Icon { get; set; }

        public string redirect { get; set; }

        public string ParentId { get; set; }
        public bool hideChildren { get; set; }

        public bool Show { get; set; }

        public bool IsBtn { get; set; }
    }
}
