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
    /// Realization of IThemeService interface.
    /// </summary>
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAnswerRepository answerRepository;

        public AnswerService(IUnitOfWork unitOfWork, IAnswerRepository answerRepository)
        {
            this.unitOfWork = unitOfWork;
            this.answerRepository = answerRepository;
        }

        /// <summary>
        /// Gets all the answers
        /// </summary>
        /// <returns>Collection of answers</returns>
        public IEnumerable<BllAnswer> GetAll()
        {
            return answerRepository.GetAll().Select(u => u.ToBllAnswer());
        }

        /// <summary>
        /// Gets the answer by id
        /// </summary>
        /// <param name="id">Answer id</param>
        /// <returns>Answer</returns>
        public BllAnswer GetById(int id)
        {
            var answer = answerRepository.GetById(id);
            if (answer == null)
                return null;
            return answer.ToBllAnswer();
        }
        /// <summary>
        /// Creates an answer
        /// </summary>
        /// <param name="entity">Answer</param>
        public void Create(BllAnswer entity)
        {
            answerRepository.Create(entity.ToDalAnswer());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Removes the answer
        /// </summary>
        /// <param name="entity">Answer</param>
        public void Delete(BllAnswer entity)
        {
            answerRepository.Delete(entity.ToDalAnswer());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the response
        /// </summary>
        /// <param name="entity">Answer</param>
        public void Update(BllAnswer entity)
        {
            answerRepository.Update(entity.ToDalAnswer());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Gets one answer by the predicate
        /// </summary>
        /// <param name="predicates">Prediate</param>
        /// <returns>Answer</returns>
        public BllAnswer GetOneByPredicate(Expression<Func<BllAnswer, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets all answers by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Collection of answers</returns>
        public IEnumerable<BllAnswer> GetAllByPredicate(Expression<Func<BllAnswer, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllAnswer, DalAnswer>(Expression.Parameter(typeof(DalAnswer), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalAnswer, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return answerRepository.GetAllByPredicate(exp).Select(answer => answer.ToBllAnswer()).ToList();
        }
    }
}
