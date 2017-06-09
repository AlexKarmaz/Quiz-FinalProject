using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class TestMapper
    {
        public static DalTest ToDalTest(this Test ormTest)
        {
            if (ormTest == null)
                return null;
            var dalTest = new DalTest()
            {
                Id = ormTest.Id,
                Title = ormTest.Title,
                Description = ormTest.Description,
                TimeLimit = ormTest.TimeLimit,
                MinToSuccess = ormTest.MinToSuccess,
                DateCreation = ormTest.DateCreation,
                UserId = ormTest.UserId,
                ThemeId = ormTest.ThemeId,
                Questions = ormTest.Questions.Select(r => r.ToDalQuestion()).ToList(),
                TestResults = ormTest.TestResults.Select(r => r.ToDalTestResult()).ToList()
            };
            return dalTest;
        }

        public static Test ToOrmTest(this DalTest dalTest)
        {
            if (dalTest == null)
                return null;
            var ormTest = new Test()
            {
                Id = dalTest.Id,
                Title = dalTest.Title,
                Description = dalTest.Description,
                TimeLimit = dalTest.TimeLimit,
                MinToSuccess = dalTest.MinToSuccess,
                DateCreation = dalTest.DateCreation,
                UserId = dalTest.UserId,
                ThemeId = dalTest.ThemeId,
                Questions = dalTest.Questions.Select(r => r.ToOrmQuestion()).ToList(),
                TestResults = dalTest.TestResults.Select(r => r.ToOrmTestResult()).ToList()
            };
            return ormTest;
        }
    }
}
