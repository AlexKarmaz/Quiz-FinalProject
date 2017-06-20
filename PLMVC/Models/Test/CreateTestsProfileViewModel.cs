using PLMVC.Models.TestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class CreateTestsProfileViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
    }
}