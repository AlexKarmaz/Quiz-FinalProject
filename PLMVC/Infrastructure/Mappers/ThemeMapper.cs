using BLL.Interface.Entities;
using PLMVC.Models.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class ThemeMapper
    {
        public static BllTheme ToBllTheme(this ThemeViewModel mvcTheme)
        {
            if (mvcTheme == null)
                return null;
            return new BllTheme()
            {
               Id = mvcTheme.Id,
               Name = mvcTheme.Name
            };
        }
    }
}