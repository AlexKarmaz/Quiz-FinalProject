using BLL.Interface.Entities;
using PLMVC.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel ToMvcUser(this BllUser bllUser)
        {
            if (bllUser == null)
                return null;
            return new UserViewModel()
            {
                Id = bllUser.Id,
                UserName = bllUser.UserName,
                Email = bllUser.Email,
                Password = bllUser.Password,
                ProfileId = bllUser.ProfileId,
            };
        }

        public static ShowUsersViewModel ToMvcAllUsers(this BllUser bllUser)
        {
            if (bllUser == null)
                return null;
            return new ShowUsersViewModel()
            {
                Id = bllUser.Id,
                UserName = bllUser.UserName,
                Email = bllUser.Email,
                RoleNames = ""
            };
        }
    }
}