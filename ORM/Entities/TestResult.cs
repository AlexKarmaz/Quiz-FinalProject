using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ORM.Entities
{
    public class TestResult
    {
        public TestResult()
        {
            DateComplete = DateTime.Now;
            Results = new List<bool>();
        }
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public TimeSpan Runtime { get; set; }
        public DateTime DateComplete { get; set; }
        public bool IsSuccess { get; set; }
        public virtual ICollection<bool> Results { get; set; }

    }
}