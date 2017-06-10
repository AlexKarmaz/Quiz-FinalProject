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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
        }

        public IEnumerable<BllUser> GetAll()
        {
            return userRepository.GetAll().Select(u => u.ToBllUser());
        }

        public BllUser GetById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
                return null;
            return user.ToBllUser();
        }
        public string[] GetRolesForUser(string username)
        {
            return userRepository.GetRolesForUser(username);
        }

        public void Create(BllUser entity, int roleId)
        {
            userRepository.Create(entity.ToDalUser(), roleId);
            unitOfWork.Commit();
        }

        public void Delete(BllUser entity)
        {
            userRepository.Delete(entity.ToDalUser());
            unitOfWork.Commit();
        }

        public void Update(BllUser entity)
        {
            userRepository.Update(entity.ToDalUser());
            unitOfWork.Commit();
        }

        public BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUser, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return userRepository.GetAllByPredicate(exp).Select(user => user.ToBllUser()).ToList();
        }
    }
}
