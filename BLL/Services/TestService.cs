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
    /// <summary>
    /// Realization of ITestService interface.
    /// </summary>
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

        /// <summary>
        /// Gets all tests
        /// </summary>
        /// <returns>Test collection</returns>
        public IEnumerable<BllTest> GetAll()
        {
            return testRepository.GetAll().Select(u => u.ToBllTest());
        }

        /// <summary>
        /// Finds a test on id
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>The test</returns>
        public BllTest GetById(int id)
        {
            var test = testRepository.GetById(id);
            if (test == null)
                return null;
            return test.ToBllTest();
        }

        /// <summary>
        /// Creates a test
        /// </summary>
        /// <param name="entity">Test</param>
        public void Create(BllTest entity)
        {
            testRepository.Create(entity.ToDalTest());
            unitOfWork.Commit();
        }
        /// <summary>
        /// Removes a test
        /// </summary>
        /// <param name="entity">Test</param>
        public void Delete(BllTest entity)
        {
            testRepository.Delete(entity.ToDalTest());
            DeleteTestQuestions(entity.Id);
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates information about the test
        /// </summary>
        /// <param name="item">Test</param>
        public void Update(BllTest item)
        {
            testRepository.Update(item.ToDalTest());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Gets one user by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>The test</returns>
        public BllTest GetOneByPredicate(Expression<Func<BllTest, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets all users by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Test collection</returns>
        public IEnumerable<BllTest> GetAllByPredicate(Expression<Func<BllTest, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllTest, DalTest>(Expression.Parameter(typeof(DalTest), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalTest, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return testRepository.GetAllByPredicate(exp).Select(test => test.ToBllTest()).ToList();
        }

        /// <summary>
        /// Removes all test questions
        /// </summary>
        /// <param name="testId">Test id</param>
        public void DeleteTestQuestions(int testId)
        {
            var questions = questionRepository.GetAllByPredicate(q => q.TestId == testId).ToList();
            foreach(var question in questions)
            {
                questionRepository.Delete(question);
            }
        }

        /// <summary>
        /// Looks for all tests on request
        /// </summary>
        /// <param name="searchString">Search request</param>
        /// <returns>Test collection</returns>
        public IEnumerable<BllTest> Search(string searchString)
        {
            List<DalTest> dalTests; 
            dalTests =  testRepository.GetAllByPredicate(t => t.Title.ToLower().Contains(searchString)|| t.Description.ToLower().Contains(searchString)).ToList();
            return dalTests.Select(t => t.ToBllTest());
        }

        /// <summary>
        /// Checks user answers
        /// </summary>
        /// <param name="testId">Test id</param>
        /// <param name="testResults">An array of user responses</param>
        /// <returns>A collection of checked questions</returns>
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

        /// <summary>
        /// Checks the readiness of the test
        /// </summary>
        /// <param name="testId">Test id</param>
        /// <returns>bool value, is the test ready or not</returns>
        public bool IsTestReady (int testId)
        {
            var test = testRepository.GetById(testId);
            bool isReady = true;
            if(test.Questions.Count < 1)
                return  false;
            foreach(var question in test.Questions)
            {
                if(question.Answers.Count < 1)
                {
                    isReady = false;
                    break;
                }
            }
            return isReady;
        }
    }
}
