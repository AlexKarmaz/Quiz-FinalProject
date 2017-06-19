using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    public class Profile
    {
        /// <summary>
        /// This ORM entity represents a profile which stores in the database.
        /// </summary>
        public Profile()
        {
            PassedTests = new List<Test>();
            CreatedTests = new List<Test>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<Test> PassedTests { get; set; }
        public virtual ICollection<Test> CreatedTests { get; set; }
    }
}