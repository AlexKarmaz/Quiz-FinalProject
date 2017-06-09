using ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.Interfaces;
using System.Data.Entity;
using DAL.Interface.DTO;
using DAL.Mappers;
using System.Linq.Expressions;
using DAL.Interface;

namespace DAL.Concrete.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DbContext context;

        public TestRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalTest> GetAll()
        {
            var tests = context.Set<Test>().Select(test => test).ToList();
            return tests.Select(u => u.ToDalTest()).ToList();
        }

        public DalTest GetById(int id)
        {
            var ormTest = context.Set<Test>().Find(id);
            return ormTest.ToDalTest();
        }

        public void Create(DalTest entity)
        {
            var test = entity.ToOrmTest();
            context.Set<Test>().Add(test);
        }

        public void Delete(DalTest entity)
        {
            var ormTest = entity.ToOrmTest();
            var test = context.Set<Test>().FirstOrDefault(u => u.Id == ormTest.Id);
            context.Set<Test>().Attach(test);
            context.Set<Test>().Remove(test);
            context.Entry(test).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalTest entity)
        {
            if (entity != null)
            {
                var testToUpdate = context.Set<Test>().FirstOrDefault(u => u.Id == entity.Id);
                var ormTest = entity.ToOrmTest();
                context.Set<Test>().Attach(testToUpdate);
                testToUpdate.Title = ormTest.Title;
                testToUpdate.Description = ormTest.Description;
                testToUpdate.ThemeId = ormTest.ThemeId;
                testToUpdate.TimeLimit = ormTest.TimeLimit;
                testToUpdate.TestResults = ormTest.TestResults;
                testToUpdate.Questions = ormTest.Questions;
                testToUpdate.MinToSuccess = ormTest.MinToSuccess;
                context.Entry(testToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public DalTest GetOneByPredicate(Expression<Func<DalTest, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalTest> GetAllByPredicate(Expression<Func<DalTest, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalTest, Test>(Expression.Parameter(typeof(Test), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Test, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<Test>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalTest()).ToList();
        }
    }
}
