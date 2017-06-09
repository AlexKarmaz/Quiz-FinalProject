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
    public class AnswerService : IAnswerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAnswerRepository answerRepository;

        public AnswerService(IUnitOfWork unitOfWork, IAnswerRepository answerRepository)
        {
            this.unitOfWork = unitOfWork;
            this.answerRepository = answerRepository;
        }

        public IEnumerable<BllAnswer> GetAll()
        {
            return answerRepository.GetAll().Select(u => u.ToBllAnswer());
        }

        public BllAnswer GetById(int id)
        {
            var answer = answerRepository.GetById(id);
            if (answer == null)
                return null;
            return answer.ToBllAnswer();
        }

        public void Create(BllAnswer entity)
        {
            answerRepository.Create(entity.ToDalAnswer());
            unitOfWork.Commit();
        }

        public void Delete(BllAnswer entity)
        {
            answerRepository.Delete(entity.ToDalAnswer());
            unitOfWork.Commit();
        }

        public void Update(BllAnswer entity)
        {
            answerRepository.Update(entity.ToDalAnswer());
            unitOfWork.Commit();
        }

        public BllAnswer GetOneByPredicate(Expression<Func<BllAnswer, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllAnswer> GetAllByPredicate(Expression<Func<BllAnswer, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllAnswer, DalAnswer>(Expression.Parameter(typeof(DalAnswer), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalAnswer, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return answerRepository.GetAllByPredicate(exp).Select(answer => answer.ToBllAnswer()).ToList();
        }
    }
}
