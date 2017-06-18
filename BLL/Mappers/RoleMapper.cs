using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class RoleMapper
    {
        public static DalRole ToDalRole(this BllRole bllRole)
        {
            if (bllRole == null)
                throw new ArgumentNullException(nameof(bllRole));
            var dalRole = new DalRole()
            {
                Id = bllRole.Id,
                Name = bllRole.Name,
            };
            return dalRole;
        }

        public static BllRole ToBllRole(this DalRole dalRole)
        {
            if (dalRole == null)
                throw new ArgumentNullException(nameof(dalRole));
            var bllRole = new BllRole()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
            };
            return bllRole;
        }
    }
}
