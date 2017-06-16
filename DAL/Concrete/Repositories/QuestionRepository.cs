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

namespace DAL.Concrete.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly DbContext context;

        public QuestionRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalQuestion> GetAll()
        {
            var questions = context.Set<Question>().Select(question => question).ToList();
            return questions.Select(u => u.ToDalQuestion()).ToList();
        }

        public DalQuestion GetById(int id)
        {
            var ormQuestion = context.Set<Question>().Find(id);
            return ormQuestion.ToDalQuestion();
        }

        public void Create(DalQuestion entity)
        {
            var question = entity.ToOrmQuestion();
            context.Set<Question>().Add(question);
        }
        public void CreateAndUpdateTestId(DalQuestion entity,int testId)
        {
            var question = entity.ToOrmQuestion();
            var test = context.Set<Test>().FirstOrDefault(t => t.Id == testId);
            test.Questions.Add(question);
            context.Set<Question>().Add(question);
        }
        
        public void Delete(DalQuestion entity)
        {
            var ormQuestion = entity.ToOrmQuestion();
            var question = context.Set<Question>().FirstOrDefault(u => u.Id == ormQuestion.Id);
            context.Set<Question>().Attach(question);
            context.Set<Question>().Remove(question);
            context.Entry(question).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalQuestion entity)
        {
            if (entity != null)
            {
                var questionToUpdate = context.Set<Question>().FirstOrDefault(u => u.Id == entity.Id);
                var ormQuestion = entity.ToOrmQuestion();
                context.Set<Question>().Attach(questionToUpdate);
                questionToUpdate.Text = ormQuestion.Text;
                context.Entry(questionToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public DalQuestion GetOneByPredicate(Expression<Func<DalQuestion, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalQuestion> GetAllByPredicate(Expression<Func<DalQuestion, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalQuestion, Question>(Expression.Parameter(typeof(Question), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Question, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<Question>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalQuestion()).ToList();
        }
    }
}
