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

namespace BLL.Services
{
    public class TestService : ITestService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITestRepository testRepository;
        private readonly IQuestionRepository questionRepositiry;

        public TestService(IUnitOfWork unitOfWork, ITestRepository testRepository, IQuestionRepository questionRepositiry)
        {
            this.unitOfWork = unitOfWork;
            this.testRepository = testRepository;
            this.questionRepositiry = questionRepositiry;
        }

        public IEnumerable<BllTest> GetAll()
        {
            return testRepository.GetAll().Select(u => u.ToBllTest());
        }

        public BllTest GetById(int id)
        {
            var test = testRepository.GetById(id);
            if (test == null)
                return null;
            return test.ToBllTest();
        }

        public void Create(BllTest entity)
        {
            testRepository.Create(entity.ToDalTest());
            unitOfWork.Commit();
        }

        public void Delete(BllTest entity)
        {
            testRepository.Delete(entity.ToDalTest());
            DeleteTestQuestions(entity.Id);
            unitOfWork.Commit();
        }

        public void Update(BllTest item)
        {
            testRepository.Update(item.ToDalTest());
            unitOfWork.Commit();
        }

        public BllTest GetOneByPredicate(Expression<Func<BllTest, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllTest> GetAllByPredicate(Expression<Func<BllTest, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllTest, DalTest>(Expression.Parameter(typeof(DalTest), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalTest, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return testRepository.GetAllByPredicate(exp).Select(test => test.ToBllTest()).ToList();
        }

        public void DeleteTestQuestions(int testId)
        {
            var questions = questionRepositiry.GetAllByPredicate(q => q.TestId == testId).ToList();
            foreach(var question in questions)
            {
                questionRepositiry.Delete(question);
               // questions.Remove(question);
            }

        }
    }
}
