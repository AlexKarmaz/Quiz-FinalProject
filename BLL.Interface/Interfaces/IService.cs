using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    /// <summary>
    /// This interface provides basic CRUD and GET operations for services.
    /// </summary>
    /// <typeparam name="T">BLL entity.</typeparam>
    public interface IService<T>
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        T GetOneByPredicate(Expression<Func<T, bool>> predicates);
        IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicates);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
