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
    /// <summary>
    /// Realization of IUserService interface.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly ITestResultRepository testResultRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, ITestResultRepository testResultRepository)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.testResultRepository = testResultRepository;
        }

        /// <summary>
        /// Finds all users
        /// </summary>
        /// <returns>IEnumerable collection of users</returns>
        public IEnumerable<BllUser> GetAll()
        {
            return userRepository.GetAll().Select(u => u.ToBllUser());
        }

        /// <summary>
        /// Finds the user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Found user</returns>
        public BllUser GetById(int id)
        {
            var user = userRepository.GetById(id);
            if (user == null)
                return null;
            return user.ToBllUser();
        }
        /// <summary>
        /// Finds all user roles
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns> Array of roles</returns>
        public string[] GetRolesForUser(string username)
        {
            return userRepository.GetRolesForUser(username);
        }

        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="entity">User </param>
        /// <param name="roleId">Roles id </param>
        public void Create(BllUser entity, int roleId)
        {
            userRepository.Create(entity.ToDalUser(), roleId);
            unitOfWork.Commit();
        }

        /// <summary>
        /// Removes the user
        /// </summary>
        /// <param name="entity">User </param>
        public void Delete(BllUser entity)
        {
            userRepository.Delete(entity.ToDalUser());
            DeleteUserResults(entity.Id);
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates user data
        /// </summary>
        /// <param name="entity">User </param>
        public void Update(BllUser entity)
        {
            userRepository.Update(entity.ToDalUser());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Gets one user by predicate
        /// </summary>
        /// <param name="predicates">Predicate </param>
        /// <returns>User</returns>
        public BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets all users by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Collection of users </returns>
        public IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllUser, DalUser>(Expression.Parameter(typeof(DalUser), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalUser, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return userRepository.GetAllByPredicate(exp).Select(user => user.ToBllUser()).ToList();
        }

        /// <summary>
        /// Removes the results of passing tests from the user.
        /// </summary>
        /// <param name="userId">User id</param>
        public void DeleteUserResults(int userId)
        {
            var userResults = testResultRepository.GetAllByPredicate(r => r.UserId == userId);
            foreach(var userResult in userResults)
            {
                testResultRepository.Delete(userResult);
            }
        }
        /// <summary>
        /// Checks the existence of the user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Bool</returns>
        public bool IsExistUser(int userId)
        {
            bool isExist = true;
            DalUser user = null;
          try
            {
                 user = userRepository.GetOneByPredicate(u => u.Id == userId);
            }
            catch(Exception)
            {
                isExist = false;
            }
            if (user == null)
                isExist = false;

            return isExist;
        }
    }
}
