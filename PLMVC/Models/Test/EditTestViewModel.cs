﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class EditTestViewModel
    {
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Title { get; set; }
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Description { get; set; }
        [Display(Name = "Time limit")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"^([0-1]\d|2[0-3])(:[0-5]\d){2}$", ErrorMessage = "The time must be in format HH:MM:SS")]
        public TimeSpan TimeLimit { get; set; }
        [Display(Name = "Min % of correct answers")]
        [Required(ErrorMessage = "The field can not be empty!")]
        [Range(0, 100, ErrorMessage = "This number must be between 0 and 100")]
        public double MinToSuccess { get; set; }
        public int ThemeId { get; set; }
    }
}