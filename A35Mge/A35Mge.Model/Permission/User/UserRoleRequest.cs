using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.Permission.User
{
    public class UserRoleRequest
    {
        public int[] RoleIds { get; set; }

        public int UserId { get; set; }
    }
}
