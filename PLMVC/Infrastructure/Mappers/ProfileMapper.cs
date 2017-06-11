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
        public static BllProfile ToBllProfile(this ProfileViewModel model)
        {
            return new BllProfile()
            {
                Id = model.Id,
                UserId = model.UserId
                //CreatedTests = model.ToBllProfile();
            };
        }

        public static ProfileViewModel ToMvcProfile(this BllProfile profile)
        {
            return new ProfileViewModel()
            {
                Id = profile.Id,
                UserId = profile.Id
            };
        }
    }
}