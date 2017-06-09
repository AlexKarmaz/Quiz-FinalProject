using PLMVC.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? ProfileId { get; set; }
       // public ProfileViewModel Profile{ get; set;}
    }
}