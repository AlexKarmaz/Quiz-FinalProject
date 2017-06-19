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
        /// <summary>
        /// Finds the user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Found user</returns>
        BllUser GetById(int id);
        /// <summary>
        /// Finds all users
        /// </summary>
        /// <returns>IEnumerable collection of users</returns>
        IEnumerable<BllUser> GetAll();
        /// <summary>
        /// Gets one user by predicate
        /// </summary>
        /// <param name="predicates">Predicate </param>
        /// <returns>User</returns>
        BllUser GetOneByPredicate(Expression<Func<BllUser, bool>> predicates);
        /// <summary>
        /// Gets all users by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Collection of users </returns>
        IEnumerable<BllUser> GetAllByPredicate(Expression<Func<BllUser, bool>> predicates);
        /// <summary>
        /// Finds all user roles
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns> Array of roles</returns>
        string[] GetRolesForUser(string username);
        /// <summary>
        /// Creates a user
        /// </summary>
        /// <param name="entity">User </param>
        /// <param name="roleId">Roles id </param>
        void Create(BllUser entity, int roleId);
        /// <summary>
        /// Removes the user
        /// </summary>
        /// <param name="entity">User </param>
        void Delete(BllUser entity);
        /// <summary>
        /// Updates user data
        /// </summary>
        /// <param name="entity">User </param>
        void Update(BllUser entity);
        /// <summary>
        /// Checks the existence of the user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>Bool</returns>
        bool IsExistUser(int userId);
    }
}
