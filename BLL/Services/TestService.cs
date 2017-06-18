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
        private readonly IQuestionRepository questionRepository;

        public TestService(IUnitOfWork unitOfWork, ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.testRepository = testRepository;
            this.questionRepository = questionRepository;
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
            var questions = questionRepository.GetAllByPredicate(q => q.TestId == testId).ToList();
            foreach(var question in questions)
            {
                questionRepository.Delete(question);
            }
        }

        public IEnumerable<BllTest> Search(string searchString)
        {
            List<DalTest> dalTests; 
            dalTests =  testRepository.GetAllByPredicate(t => t.Title.ToLower().Contains(searchString)).ToList();
            var dalTestsByDescription = testRepository.GetAllByPredicate(t => t.Description.ToLower().Contains(searchString)).ToList();
         
            if (dalTests.Count() < dalTestsByDescription.Count())
            {
                dalTests = dalTestsByDescription.Union(dalTests).ToList();
            }else
            {
                dalTests.Union(dalTestsByDescription);
            }
            return dalTests.Select(t => t.ToBllTest());
        }

        public IEnumerable<bool> CheckAnswers(int testId, bool[][] testResults)
        {
            var questions = testRepository.GetById(testId).Questions;
            int i = 0;
            bool[] results = new bool[questions.Count];

            foreach(var question in questions)
            {
                results[i] = true;
                var answers = question.Answers;
                int j = 0;
                foreach(var answer in answers)
                {
                    if (testResults[i][j] != answer.IsRight)
                    {
                        results[i] = false;
                        break;
                    }
                    j++;
                }
                i++;
            }
            return results;
        }
    }
}
