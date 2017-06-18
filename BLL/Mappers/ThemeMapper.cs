using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class ThemeMapper
    {
        public static DalTheme ToDalTheme(this BllTheme bllTheme)
        {
            if (bllTheme == null)
                throw new ArgumentNullException(nameof(bllTheme));
            var dalTheme = new DalTheme()
            {
                Id = bllTheme.Id,
                Name = bllTheme.Name,
            };
            return dalTheme;
        }

        public static BllTheme ToBllTheme(this DalTheme dalTheme)
        {
            if (dalTheme == null)
                throw new ArgumentNullException(nameof(dalTheme));
            var bllTheme = new BllTheme()
            {
                Id = dalTheme.Id,
                Name = dalTheme.Name
            };
            return bllTheme;
        }
    }
}
