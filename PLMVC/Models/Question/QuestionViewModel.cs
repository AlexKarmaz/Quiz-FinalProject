using PLMVC.Models.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public string Text { get; set; }
        public int? TestId { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}