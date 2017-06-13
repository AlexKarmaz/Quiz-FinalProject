using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalQuestion : IEntity
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public string Text { get; set; }
        public int? TestId { get; set; }
        public ICollection<DalAnswer> Answers { get; set; }
    }
}
