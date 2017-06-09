using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    public class User
    {
        public User()
        {
            Roles = new List<Role>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}