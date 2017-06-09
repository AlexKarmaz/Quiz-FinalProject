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
    public class TestResultRepository : ITestResultRepository
    {
        private readonly DbContext context;

        public TestResultRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalTestResult> GetAll()
        {
            var testResults = context.Set<TestResult>().Select(testResult => testResult).ToList();
            return testResults.Select(u => u.ToDalTestResult()).ToList();
        }

        public DalTestResult GetById(int id)
        {
            var ormTestResult = context.Set<TestResult>().Find(id);
            return ormTestResult.ToDalTestResult();
        }

        public void Create(DalTestResult entity)
        {
            var testResult = entity.ToOrmTestResult();
            context.Set<TestResult>().Add(testResult);
        }

        public void Delete(DalTestResult entity)
        {
            var ormTestResult = entity.ToOrmTestResult();
            var testResult = context.Set<TestResult>().FirstOrDefault(u => u.Id == ormTestResult.Id);
            context.Set<TestResult>().Attach(testResult);
            context.Set<TestResult>().Remove(testResult);
            context.Entry(testResult).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalTestResult entity)
        {
            throw new NotImplementedException();
            //if (entity != null)
            //{
            //    var testResultToUpdate = context.Set<TestResult>().FirstOrDefault(u => u.Id == entity.Id);
            //    var ormTestResult = entity.ToOrmTestResult();
            //    context.Set<TestResult>().Attach(testResultToUpdate);
            //    testToUpdate.Title = ormTest.Title;
            //    testToUpdate.Description = ormTest.Description;
            //    testToUpdate.ThemeId = ormTest.ThemeId;
            //    testToUpdate.TimeLimit = ormTest.TimeLimit;
            //    testToUpdate.TestResults = ormTest.TestResults;
            //    testToUpdate.Questions = ormTest.Questions;
            //    testToUpdate.MinToSuccess = ormTest.MinToSuccess;
            //    context.Entry(testToUpdate).State = System.Data.Entity.EntityState.Modified;
            //}
        }

        public DalTestResult GetOneByPredicate(Expression<Func<DalTestResult, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalTestResult> GetAllByPredicate(Expression<Func<DalTestResult, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalTestResult, TestResult>(Expression.Parameter(typeof(TestResult), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<TestResult, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<TestResult>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalTestResult()).ToList();
        }
    }
}
