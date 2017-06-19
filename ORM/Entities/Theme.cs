using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    /// <summary>
    /// This ORM entity represents a theme which stores in the database.
    /// </summary>
    public class Theme
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}