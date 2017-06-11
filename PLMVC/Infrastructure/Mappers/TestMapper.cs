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
    }
}