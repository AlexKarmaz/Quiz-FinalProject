using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class BllQuestion
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public string Text { get; set; }
        public int? TestId { get; set; }
        public ICollection<BllAnswer> Answers { get; set; }
    }
}
