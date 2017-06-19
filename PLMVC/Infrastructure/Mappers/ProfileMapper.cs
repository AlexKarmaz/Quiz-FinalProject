using BLL.Interface.Entities;
using PLMVC.Models.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class ProfileMapper
    {
        public static BllProfile ToBllProfile(this ProfileViewModel profile)
        {
            if (profile == null)
                return null;
            return new BllProfile()
            {
                Id = profile.Id,
                UserId = profile.UserId
            };
        }

        public static ProfileViewModel ToMvcProfile(this BllProfile profile)
        {
            if (profile == null)
                return null;
            return new ProfileViewModel()
            {
                Id = profile.Id,
                UserId = profile.Id
            };
        }
    }
}