using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class ThemeMapper
    {
        public static DalTheme ToDalTheme(this Theme ormTheme)
        {
            if (ormTheme == null)
                return null;
            var dalTheme = new DalTheme()
            {
                Id = ormTheme.Id,
                Name = ormTheme.Name,
            };
            return dalTheme;
        }

        public static Theme ToOrmTheme(this DalTheme dalTheme)
        {
            if (dalTheme == null)
                return null;
            var ormTheme = new Theme()
            {
                Id = dalTheme.Id,
                Name = dalTheme.Name
            };
            return ormTheme;
        }
    }
}
