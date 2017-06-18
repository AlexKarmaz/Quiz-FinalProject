using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class RoleMapper
    {
        public static DalRole ToDalRole(this Role ormRole)
        {
            if (ormRole == null)
                throw new ArgumentNullException(nameof(ormRole));
            var dalRole = new DalRole()
            {
                Id = ormRole.Id,
                Name = ormRole.Name,
            };
            return dalRole;
        }

        public static Role ToOrmRole(this DalRole dalRole)
        {
            if (dalRole == null)
                throw new ArgumentNullException(nameof(dalRole));
            var ormRole = new Role()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
            return ormRole;
        }
    }
}
