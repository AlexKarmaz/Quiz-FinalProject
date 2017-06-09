
using ORM.Entities;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ORM
{
    public class TestEntities : DbContext
    {
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