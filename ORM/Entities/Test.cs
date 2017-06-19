using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    /// <summary>
    /// This ORM entity represents a test which stores in the database.
    /// </summary>
    public class Test
    {
        public Test()
        {
            DateCreation = DateTime.Now;
            Questions = new List<Question>();
            TestResults = new List<TestResult>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public double MinToSuccess { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Theme")]
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}