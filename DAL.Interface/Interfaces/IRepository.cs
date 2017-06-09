using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DAL.Interface.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetOneByPredicate(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicate);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
