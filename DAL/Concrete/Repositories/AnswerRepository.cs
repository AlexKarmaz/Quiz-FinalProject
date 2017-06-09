using DAL.Interface;
using DAL.Interface.DTO;
using DAL.Interface.Interfaces;
using DAL.Mappers;
using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly DbContext context;

        public AnswerRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalAnswer> GetAll()
        {
            var answers = context.Set<Answer>().Select(answer => answer).ToList();
            return answers.Select(u => u.ToDalAnswer()).ToList();
        }

        public DalAnswer GetById(int id)
        {
            var ormAnswer = context.Set<Answer>().Find(id);
            return ormAnswer.ToDalAnswer();
        }

        public void Create(DalAnswer entity)
        {
            var answer = entity.ToOrmAnswer();
            context.Set<Answer>().Add(answer);
        }

        public void Delete(DalAnswer entity)
        {
            var ormAnswer = entity.ToOrmAnswer();
            var answer = context.Set<Answer>().FirstOrDefault(u => u.Id == ormAnswer.Id);
            context.Set<Answer>().Attach(answer);
            context.Set<Answer>().Remove(answer);
            context.Entry(answer).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalAnswer entity)
        {
            if (entity != null)
            {
                var answerToUpdate = context.Set<Answer>().FirstOrDefault(u => u.Id == entity.Id);
                var ormAnswer = entity.ToOrmAnswer();
                context.Set<Answer>().Attach(answerToUpdate);
                answerToUpdate.Text = ormAnswer.Text;
                answerToUpdate.IsRight = ormAnswer.IsRight;
                context.Entry(answerToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public DalAnswer GetOneByPredicate(Expression<Func<DalAnswer, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalAnswer> GetAllByPredicate(Expression<Func<DalAnswer, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalAnswer, Answer>(Expression.Parameter(typeof(Answer), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Answer, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<Answer>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalAnswer()).ToList();
        }
    }
}
