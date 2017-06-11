using PLMVC.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class CreateTestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public double MinToSuccess { get; set; }
        public int ThemeId { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}