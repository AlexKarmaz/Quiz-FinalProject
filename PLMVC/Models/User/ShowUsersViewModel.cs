using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.User
{
    public class ShowUsersViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleNames { get; set; }
    }
}