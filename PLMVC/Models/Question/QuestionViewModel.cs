using PLMVC.Models.Answer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Question
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Text { get; set; }
        public int? TestId { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}