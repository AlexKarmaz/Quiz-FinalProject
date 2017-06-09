using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this BllUser bllUser)
        {
            if (bllUser == null)
                return null;
            var dalUser = new DalUser()
            {
                Id = bllUser.Id,
                UserName = bllUser.UserName,
                Password = bllUser.Password,
                Email = bllUser.Email,
                ProfileId = bllUser.ProfileId,
                Profile = bllUser.Profile.ToDalProfile(),
                Roles = bllUser.Roles.Select(r => r.ToDalRole()).ToList()
            };
            return dalUser;
        }

        public static BllUser ToBllUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;
            var bllUser = new BllUser()
            {
                Id = dalUser.Id,
                UserName = dalUser.UserName,
                Password = dalUser.Password,
                Email = dalUser.Email,
                ProfileId = dalUser.ProfileId,
                Profile = dalUser.Profile.ToBllProfile(),
                Roles = dalUser.Roles.Select(r => r.ToBllRole()).ToList()
            };
            return bllUser;
        }
    }
}
