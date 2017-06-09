using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class ProfileMapper
    {
        public static DalProfile ToDalProfile(this BllProfile bllProfile)
        {
            if (bllProfile == null)
                return null;
            return new DalProfile()
            {
                Id = bllProfile.Id,
                UserId = bllProfile.UserId,
                PassedTests = bllProfile.PassedTests.Select(r => r.ToDalTest()).ToList(),
                CreatedTests = bllProfile.CreatedTests.Select(r => r.ToDalTest()).ToList()
            };

        }
        public static BllProfile ToBllProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null)
                return null;
            return new BllProfile()
            {
                Id = dalProfile.Id,
                UserId = dalProfile.UserId,
                PassedTests = dalProfile.PassedTests.Select(r => r.ToBllTest()).ToList(),
                CreatedTests = dalProfile.CreatedTests.Select(r => r.ToBllTest()).ToList()
            };
        }

    }
}
