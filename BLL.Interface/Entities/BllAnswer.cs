using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// This class represents an answer on Business Logic Layer.
    /// </summary>
    public class BllAnswer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }
    }
}
