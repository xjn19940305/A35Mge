using A35Mge.Model.common;
using System;
using System.Collections.Generic;
using System.Text;

namespace A35Mge.Model.Permission.User
{
    public class UserRequest : PagingModel
    {
        public string Account { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string NickName { get; set; }
    }
}
