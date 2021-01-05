using A35Mge.Model.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.Permission.Role
{
    public class RoleRequestDTO : PagingModel
    {
        public string RoleName { get; set; }
    }
}
