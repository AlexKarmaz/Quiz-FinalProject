using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    /// <summary>
    /// This class represents a profile on Business Logic Layer.
    /// </summary>
    public class BllProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<BllTest> PassedTests { get; set; }
        public ICollection<BllTest> CreatedTests { get; set; }
    }
}
