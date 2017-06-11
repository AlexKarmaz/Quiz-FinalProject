using BLL.Interface.Entities;
using PLMVC.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class TestMapper
    {
        public static BllTest ToBllTest(this CreateTestViewModel createTestViewModel)
        {
            if (createTestViewModel == null)
                return null;
            return new BllTest()
            {
                Title = createTestViewModel.Title,
                Description = createTestViewModel.Description,
                TimeLimit = createTestViewModel.TimeLimit,
                MinToSuccess = createTestViewModel.MinToSuccess,
                ThemeId = createTestViewModel.ThemeId,
                Questions = createTestViewModel.Questions.Select(r => r.ToBllQuestion()).ToList()
            };
        }

        public static DetailsTestViewModel ToMvcTest(this BllTest bllTest)
        {
            if (bllTest == null)
                return null;
            return new DetailsTestViewModel()
            {
                Title = bllTest.Title,
                Description = bllTest.Description,
                TimeLimit = bllTest.TimeLimit,
                MinToSuccess = bllTest.MinToSuccess,
                DateCreation = bllTest.DateCreation,
                ThemeName = bllTest.ThemeId.ToString(),
                UserName = bllTest.UserId.ToString(),
                Questions = bllTest.Questions.Select(r => r.ToMvcQuestion()).ToList()
            };
        }
    }
}