using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalProfile : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<DalTest> PassedTests { get; set; }
        public virtual ICollection<DalTest> CreatedTests { get; set; }
    }
}
