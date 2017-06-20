using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Theme
{
    public class ThemeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The field can not be empty!")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }
    }
}