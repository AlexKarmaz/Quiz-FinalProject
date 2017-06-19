using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// This class represents a test on Business Logic Layer.
    /// </summary>
    public class BllTest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public double MinToSuccess { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }
        public int ThemeId { get; set; }
        public ICollection<BllQuestion> Questions { get; set; }
        public ICollection<BllTestResult> TestResults { get; set; }
    }
}
