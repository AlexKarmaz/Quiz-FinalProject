using PLMVC.Models.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class PassingTestViewModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
        public DateTime StartTest { get; set; }
        public DateTime FinishTest { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
        public bool[][] Results { get; set; }
    }
}