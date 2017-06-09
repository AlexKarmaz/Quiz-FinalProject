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
    public class TestResultService : ITestResultService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITestResultRepository testResultRepository;

        public TestResultService(IUnitOfWork unitOfWork, ITestResultRepository testResultRepository)
        {
            this.unitOfWork = unitOfWork;
            this.testResultRepository = testResultRepository;
        }

        public IEnumerable<BllTestResult> GetAll()
        {
            return testResultRepository.GetAll().Select(u => u.ToBllTestResult());
        }

        public BllTestResult GetById(int id)
        {
            var testResult = testResultRepository.GetById(id);
            if (testResult == null)
                return null;
            return testResult.ToBllTestResult();
        }

        public void Create(BllTestResult entity)
        {
            testResultRepository.Create(entity.ToDalTestResult());
            unitOfWork.Commit();
        }

        public void Delete(BllTestResult entity)
        {
            testResultRepository.Delete(entity.ToDalTestResult());
            unitOfWork.Commit();
        }

        public void Update(BllTestResult item)
        {
            throw new NotImplementedException();
        }

        public BllTestResult GetOneByPredicate(Expression<Func<BllTestResult, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllTestResult> GetAllByPredicate(Expression<Func<BllTestResult, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllTestResult, DalTestResult>(Expression.Parameter(typeof(DalTestResult), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalTestResult, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return testResultRepository.GetAllByPredicate(exp).Select(testResult => testResult.ToBllTestResult()).ToList();
        }
    }
}
