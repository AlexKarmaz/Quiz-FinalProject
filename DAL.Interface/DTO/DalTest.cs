using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.DTO
{
    public class DalTest : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public double MinToSuccess { get; set; }
        public DateTime DateCreation { get; set; }
        public int UserId { get; set; }
        public int ThemeId { get; set; }
        public ICollection<DalQuestion> Questions { get; set; }
        public ICollection<DalTestResult> TestResults { get; set; }
    }
}
