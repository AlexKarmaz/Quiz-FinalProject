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
    /// Realization of IRoleService interface.
    /// </summary>
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
        }

        /// <summary>
        /// Verifies that the user belongs to the role
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="roleName">Rolename</param>
        /// <returns>bool value</returns>
        public bool IsUserInRole(string userName, string roleName) => roleRepository.IsUserInRole(userName, roleName);

        /// <summary>
        /// Gets all the roles
        /// </summary>
        /// <returns>Collection of roles</returns>
        public IEnumerable<BllRole> GetAll()
        {
            return roleRepository.GetAll().Select(r => r.ToBllRole());
        }

        /// <summary>
        /// Gets a role on id
        /// </summary>
        /// <param name="id">Role id</param>
        /// <returns>The role</returns>
        public BllRole GetById(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }

        /// <summary>
        /// Creates a role
        /// </summary>
        /// <param name="entity">Role</param>
        public void Create(BllRole entity)
        {
            roleRepository.Create(entity.ToDalRole());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Removes the role
        /// </summary>
        /// <param name="entity">Role</param>
        public void Delete(BllRole entity)
        {
            roleRepository.Delete(entity.ToDalRole());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates the role
        /// </summary>
        /// <param name="entity">Role</param>
        public void Update(BllRole entity)
        {
            roleRepository.Update(entity.ToDalRole());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Gets one role by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Role</returns>
        public BllRole GetOneByPredicate(Expression<Func<BllRole, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets all the roles by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Collection of roles</returns>
        public IEnumerable<BllRole> GetAllByPredicate(Expression<Func<BllRole, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllRole, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return roleRepository.GetAllByPredicate(exp).Select(role => role.ToBllRole()).ToList();
        }
    }
}
