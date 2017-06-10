using DAL.Interface.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interface.Interfaces
{
    public  interface IUserRepository 
    {
        void Create(DalUser dalUser, int roleId);
        IEnumerable<DalUser> GetAll();
        DalUser GetById(int id);
        string[] GetRolesForUser(string userName);
        DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate);
        IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate);
        void Delete(DalUser entity);
        void Update(DalUser entity);
    }
}
