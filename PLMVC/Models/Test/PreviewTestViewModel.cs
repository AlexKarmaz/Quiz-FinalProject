using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Models.Test
{
    public class PreviewTestViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public TimeSpan TimeLimit { get; set; }
        public double MinToSuccess { get; set; }
        
    }
      
}