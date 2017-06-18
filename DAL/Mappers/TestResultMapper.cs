using DAL.Interface.DTO;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Mappers
{
    public static class TestResultMapper
    {
        public static DalTestResult ToDalTestResult(this TestResult ormTestResult)
        {
            if (ormTestResult == null)
                throw new ArgumentNullException(nameof(ormTestResult));
            var dalTestResult = new DalTestResult()
            {
                Id = ormTestResult.Id,
                TestId = ormTestResult.TestId,
                UserId = ormTestResult.UserId,
                Runtime = ormTestResult.Runtime,
                DateComplete = ormTestResult.DateComplete,
                IsSuccess = ormTestResult.IsSuccess,
                Results = ormTestResult.Results
            };
            return dalTestResult;
        }

        public static TestResult ToOrmTestResult(this DalTestResult dalTestResult)
        {
            if (dalTestResult == null)
                throw new ArgumentNullException(nameof(dalTestResult));
            var ormTestResult = new TestResult()
            {
                Id = dalTestResult.Id,
                TestId = dalTestResult.TestId,
                UserId = dalTestResult.UserId,
                Runtime = dalTestResult.Runtime,
                DateComplete = dalTestResult.DateComplete,
                IsSuccess = dalTestResult.IsSuccess,
                Results = dalTestResult.Results
            };
            return ormTestResult;
        }
    }
}
