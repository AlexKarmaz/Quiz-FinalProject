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
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuestionRepository questionRepository;

        public QuestionService(IUnitOfWork unitOfWork, IQuestionRepository questionRepository)
        {
            this.unitOfWork = unitOfWork;
            this.questionRepository = questionRepository;
        }

        public IEnumerable<BllQuestion> GetAll()
        {
            return questionRepository.GetAll().Select(u => u.ToBllQuestion());
        }

        public BllQuestion GetById(int id)
        {
            var question = questionRepository.GetById(id);
            if (question == null)
                return null;
            return question.ToBllQuestion();
        }

        public void Create(BllQuestion entity)
        {
            questionRepository.Create(entity.ToDalQuestion());
            unitOfWork.Commit();
        }
        public void CreateAndUpdateTestId(BllQuestion entity, int testId)
        {
            questionRepository.CreateAndUpdateTestId(entity.ToDalQuestion(), testId);
            unitOfWork.Commit();
        }

        public void Delete(BllQuestion entity)
        {
            questionRepository.Delete(entity.ToDalQuestion());
            unitOfWork.Commit();
        }

        public void Update(BllQuestion entity)
        {
            questionRepository.Update(entity.ToDalQuestion());
            unitOfWork.Commit();
        }

        public BllQuestion GetOneByPredicate(Expression<Func<BllQuestion, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllQuestion> GetAllByPredicate(Expression<Func<BllQuestion, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllQuestion, DalQuestion>(Expression.Parameter(typeof(DalQuestion), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalQuestion, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return questionRepository.GetAllByPredicate(exp).Select(question => question.ToBllQuestion()).ToList();
        }
    }
}