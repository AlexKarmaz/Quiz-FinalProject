using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    /// <summary>
    /// This ORM entity represents a role which stores in the database.
    /// </summary>
    public class Role
    {
        public Role()
        {
            Users = new List<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}