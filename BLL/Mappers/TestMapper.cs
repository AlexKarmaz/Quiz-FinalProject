using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class TestMapper
    {
        public static DalTest ToDalTest(this BllTest bllTest)
        {
            if (bllTest == null)
                return null;
            var dalTest = new DalTest()
            {
                Id = bllTest.Id,
                Title = bllTest.Title,
                Description = bllTest.Description,
                TimeLimit = bllTest.TimeLimit,
                MinToSuccess = bllTest.MinToSuccess,
                DateCreation = bllTest.DateCreation,
                UserId = bllTest.UserId,
                ThemeId = bllTest.ThemeId,
                Questions = bllTest.Questions.Select(r => r.ToDalQuestion()).ToList(),
                TestResults = bllTest.TestResults.Select(r => r.ToDalTestResult()).ToList()
            };
            return dalTest;
        }

        public static BllTest ToBllTest(this DalTest dalTest)
        {
            if (dalTest == null)
                return null;
            var bllTest = new BllTest()
            {
                Id = dalTest.Id,
                Title = dalTest.Title,
                Description = dalTest.Description,
                TimeLimit = dalTest.TimeLimit,
                MinToSuccess = dalTest.MinToSuccess,
                DateCreation = dalTest.DateCreation,
                UserId = dalTest.UserId,
                ThemeId = dalTest.ThemeId,
                Questions = dalTest.Questions.Select(r => r.ToBllQuestion()).ToList(),
                TestResults = dalTest.TestResults.Select(r => r.ToBllTestResult()).ToList()
            };
            return bllTest;
        }
    }
}
