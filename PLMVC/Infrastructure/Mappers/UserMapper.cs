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
            return new UserViewModel()
            {
                Id = bllUser.Id,
                UserName = bllUser.UserName,
                Email = bllUser.Email,
                Password = bllUser.Password,
                ProfileId = bllUser.ProfileId,
               // Profile = bllUser.Profile.ToMvcProfile()
            };
        }
    }
}