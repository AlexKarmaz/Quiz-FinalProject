using BLL.Interface.Entities;
using PLMVC.Models.TestResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class TestResultMapper
    {
        public static ResultStatisticsViewModel ToMvcStatistics(this BllTestResult bllTestResult)
        {
            if (bllTestResult == null)
                return null;
            return new ResultStatisticsViewModel()
            {
                UserId = bllTestResult.UserId,
                Runtime = bllTestResult.Runtime,
                IsSuccess = bllTestResult.IsSuccess,
            };
        }
    }
}