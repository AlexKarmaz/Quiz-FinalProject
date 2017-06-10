using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface IUserService
    {
        BllUser GetById(int id);
        IEnumerable<BllUser> GetAll();
        BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates);
        IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates);
        string[] GetRolesForUser(string username);
        void Create(BllUser entity, int roleId);
        void Delete(BllUser entity);
        void Update(BllUser entity);
    }
}
