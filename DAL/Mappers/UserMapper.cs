using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class UserMapper
    {
        public static DalUser ToDalUser(this User ormUser)
        {
            if (ormUser == null)
                return null;
            var dalUser = new DalUser()
            {
                Id = ormUser.Id,
                UserName = ormUser.UserName,
                Password = ormUser.Password,
                Email = ormUser.Email,
                ProfileId = ormUser.ProfileId,
                Profile = ormUser.Profile.ToDalProfile()
            };
            return dalUser;
        }

        public static User ToOrmUser(this DalUser dalUser)
        {
            if (dalUser == null)
                return null;
            var ormUser = new User()
            {
                Id = dalUser.Id,
                UserName = dalUser.UserName,
                Password = dalUser.Password,
                Email = dalUser.Email,
                ProfileId = dalUser.ProfileId,
                Profile = dalUser.Profile.ToOrmProfile(),
            };
            return ormUser;
        }
    }
}
