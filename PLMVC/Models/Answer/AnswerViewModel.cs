using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Answer
{
    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsRight { get; set; }
        public int QuestionId { get; set; }
    }
}