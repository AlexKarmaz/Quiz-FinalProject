using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class ProfileMapper
    {
        public static DalProfile ToDalProfile(this Profile ormProfile)
        {
            if (ormProfile == null)
                throw new ArgumentNullException(nameof(ormProfile));
            return new DalProfile()
            {
                Id = ormProfile.Id,
                UserId = ormProfile.UserId,
                PassedTests = ormProfile.PassedTests.Select(r => r.ToDalTest()).ToList(),
                CreatedTests = ormProfile.CreatedTests.Select(r => r.ToDalTest()).ToList()
            };

        }
        public static Profile ToOrmProfile(this DalProfile dalProfile)
        {
            if (dalProfile == null)
                throw new ArgumentNullException(nameof(dalProfile));
            return new Profile()
            {
                Id = dalProfile.Id,
                UserId = dalProfile.UserId,
                PassedTests = dalProfile.PassedTests.Select(r => r.ToOrmTest()).ToList(),
                CreatedTests = dalProfile.CreatedTests.Select(r => r.ToOrmTest()).ToList()
            };
        }

    }
}
