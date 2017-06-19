
using ORM.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ORM
{
    /// <summary>
    /// A DbContext instance represents a combination of the Unit Of Work and Repository patterns such 
    /// that it can be used to query from a database and group together changes that will then 
    /// be written back to the store as a unit.
    /// </summary>
    public class TestEntities : DbContext
    {
        /// <summary>
        /// Default constructor. 
        /// It refers to the base constructor, passing to it the connection string.
        /// </summary>
        public TestEntities() : base("name =TestEntities")
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Theme> Themes { get; set; }
    }
}