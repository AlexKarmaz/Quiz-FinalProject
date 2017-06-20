using PLMVC.Models.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class DetailsTestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Time limit")]
        public TimeSpan TimeLimit { get; set; }
        [Display(Name = "Min % of correct answers")]
        public double MinToSuccess { get; set; }
        [Display(Name = "Date creation")]
        public DateTime DateCreation { get; set; }
        [Display(Name = "Creator's name")]
        public string UserName { get; set; }
        [Display(Name = "Category")]
        public string ThemeName { get; set; }
        public ICollection<QuestionViewModel> Questions { get; set; }
    }
}