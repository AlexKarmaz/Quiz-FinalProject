using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class EditTestViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public double MinToSuccess { get; set; }
        public int ThemeId { get; set; }
    }
}