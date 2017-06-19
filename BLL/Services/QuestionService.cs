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
    /// Realization of  IQuestionService interface.
    /// </summary>
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuestionRepository questionRepository;

        public QuestionService(IUnitOfWork unitOfWork, IQuestionRepository questionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.questionRepository = questionRepository;
        }

        /// <summary>
        /// Gets all the questions
        /// </summary>
        /// <returns>Collection of questions</returns>
        public IEnumerable<BllQuestion> GetAll()
        {
            return questionRepository.GetAll().Select(u => u.ToBllQuestion());
        }

        /// <summary>
        /// Gets a question on id
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Question</returns>
        public BllQuestion GetById(int id)
        {
            var question = questionRepository.GetById(id);
            if (question == null)
                return null;
            return question.ToBllQuestion();
        }

        /// <summary>
        /// Creates a question
        /// </summary>
        /// <param name="entity">Question</param>
        public void Create(BllQuestion entity)
        {
            questionRepository.Create(entity.ToDalQuestion());
            unitOfWork.Commit();
        }
        /// <summary>
        /// Creates a question and updates the test id
        /// </summary>
        /// <param name="entity">Test</param>
        /// <param name="testId">Test id</param>
        public void CreateAndUpdateTestId(BllQuestion entity, int testId)
        {
            questionRepository.CreateAndUpdateTestId(entity.ToDalQuestion(), testId);
            unitOfWork.Commit();
        }

        /// <summary>
        /// Removes a question
        /// </summary>
        /// <param name="entity">Question</param>
        public void Delete(BllQuestion entity)
        {
            questionRepository.Delete(entity.ToDalQuestion());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the question
        /// </summary>
        /// <param name="entity">Question</param>
        public void Update(BllQuestion entity)
        {
            questionRepository.Update(entity.ToDalQuestion());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Gets one question by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Question</returns>
        public BllQuestion GetOneByPredicate(Expression<Func<BllQuestion, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets all questions by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Collection of questions</returns>
        public IEnumerable<BllQuestion> GetAllByPredicate(Expression<Func<BllQuestion, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllQuestion, DalQuestion>(Expression.Parameter(typeof(DalQuestion), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalQuestion, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return questionRepository.GetAllByPredicate(exp).Select(question => question.ToBllQuestion()).ToList();
        }
    }
}