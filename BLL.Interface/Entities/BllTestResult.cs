using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllTestResult
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public TimeSpan Runtime { get; set; }
        public DateTime DateComplete { get; set; }
        public bool IsSuccess { get; set; }
        public virtual ICollection<bool> Results { get; set; }
    }
}
