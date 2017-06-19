using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// This class represents a question on Business Logic Layer.
    /// </summary>
    public class BllQuestion
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public string Text { get; set; }
        public int? TestId { get; set; }
        public ICollection<BllAnswer> Answers { get; set; }
    }
}
