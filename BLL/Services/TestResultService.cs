using BLL.Interface.Entities;
using BLL.Interface.Interfaces;
using BLL.Mappers;
using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    ///  Realization of ITestResultService interface.
    /// </summary>
    public class TestResultService : ITestResultService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITestResultRepository testResultRepository;

        public TestResultService(IUnitOfWork unitOfWork, ITestResultRepository testResultRepository)
        {
            this.unitOfWork = unitOfWork;
            this.testResultRepository = testResultRepository;
        }

        /// <summary>
        /// Gets all test results
        /// </summary>
        /// <returns>Collection of results</returns>
        public IEnumerable<BllTestResult> GetAll()
        {
            return testResultRepository.GetAll().Select(u => u.ToBllTestResult());
        }

        /// <summary>
        /// Gets test results by id
        /// </summary>
        /// <param name="id">Test result id</param>
        /// <returns>Test result</returns>
        public BllTestResult GetById(int id)
        {
            var testResult = testResultRepository.GetById(id);
            if (testResult == null)
                return null;
            return testResult.ToBllTestResult();
        }

        /// <summary>
        /// Creates a test result
        /// </summary>
        /// <param name="entity">Test result</param>
        public void Create(BllTestResult entity)
        {
            testResultRepository.Create(entity.ToDalTestResult());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes test results
        /// </summary>
        /// <param name="entity">Test result</param>
        public void Delete(BllTestResult entity)
        {
            testResultRepository.Delete(entity.ToDalTestResult());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates test results
        /// </summary>
        /// <param name="item"> Test result</param>
        public void Update(BllTestResult item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the results of test on the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>One test results</returns>
        public BllTestResult GetOneByPredicate(Expression<Func<BllTestResult, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets the results of tests on the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Test results collection</returns>
        public IEnumerable<BllTestResult> GetAllByPredicate(Expression<Func<BllTestResult, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllTestResult, DalTestResult>(Expression.Parameter(typeof(DalTestResult), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalTestResult, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return testResultRepository.GetAllByPredicate(exp).Select(testResult => testResult.ToBllTestResult()).ToList();
        }

        /// <summary>
        /// Calculates the percentage of correct answers and compares them with the minimum required
        /// </summary>
        /// <param name="results">A collection of user responses</param>
        /// <param name="minToSuccess">The minimum percentage of correct answers</param>
        /// <returns>bool value</returns>
        public bool CheckPercentAnswers( IEnumerable<bool> results, double minToSuccess)
        {
            bool isSuccess = true;
            //foreach(bool result in results)
            //{
            //    if(result == false)
            //    {
            //        isSuccess = false;
            //        break;
            //    }
            //}
            double goodAnswers = results.Where(r => r == true).Count();
            double percent = (goodAnswers / results.Count())*100;
            if (percent < minToSuccess)
                isSuccess = false;

            return isSuccess;
        }

        /// <summary>
        /// Verifies the time spent by the user and the time limit
        /// </summary>
        /// <param name="timeLimit">Time limit</param>
        /// <param name="userTime">Time spent by the user to pass the test</param>
        /// <returns>bool value</returns>
        public bool CheckTime(TimeSpan timeLimit, TimeSpan userTime)
        {
            return userTime <= timeLimit;
        }
        /// <summary>
        /// Calculates the percentage of correct user responses
        /// </summary>
        /// <param name="results">A collection of user responses</param>
        /// <returns>Percent of correct answers</returns>
        public double GetPercentGoodAnswers(IEnumerable<bool> results)
        {
            double goodAnswers = results.Where(r => r == true).Count();
         
            return (goodAnswers / results.Count()) * 100;
        }
    }
}
