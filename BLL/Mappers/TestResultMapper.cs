using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class TestResultMapper
    {
        public static DalTestResult ToDalTestResult(this BllTestResult bllTestResult)
        {
            if (bllTestResult == null)
                return null;
            var dalTestResult = new DalTestResult()
            {
                Id = bllTestResult.Id,
                TestId = bllTestResult.TestId,
                UserId = bllTestResult.UserId,
                Runtime = bllTestResult.Runtime,
                DateComplete = bllTestResult.DateComplete,
                IsSuccess = bllTestResult.IsSuccess,
                Results = bllTestResult.Results
            };
            return dalTestResult;
        }

        public static BllTestResult ToBllTestResult(this DalTestResult dalTestResult)
        {
            if (dalTestResult == null)
                return null;
            var bllTestResult = new BllTestResult()
            {
                Id = dalTestResult.Id,
                TestId = dalTestResult.TestId,
                UserId = dalTestResult.UserId,
                Runtime = dalTestResult.Runtime,
                DateComplete = dalTestResult.DateComplete,
                IsSuccess = dalTestResult.IsSuccess,
                Results = dalTestResult.Results
            };
            return bllTestResult;
        }
    }
}
