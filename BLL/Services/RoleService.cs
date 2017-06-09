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
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRoleRepository roleRepository;

        public RoleService(IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            this.unitOfWork = unitOfWork;
            this.roleRepository = roleRepository;
        }

        public IEnumerable<BllRole> GetAll()
        {
            return roleRepository.GetAll().Select(r => r.ToBllRole());
        }

        public BllRole GetById(int id)
        {
            return roleRepository.GetById(id).ToBllRole();
        }

        public void Create(BllRole entity)
        {
            roleRepository.Create(entity.ToDalRole());
            unitOfWork.Commit();
        }

        public void Delete(BllRole entity)
        {
            roleRepository.Delete(entity.ToDalRole());
            unitOfWork.Commit();
        }

        public void Update(BllRole entity)
        {
            throw new NotImplementedException();
        }

        public BllRole GetOneByPredicate(Expression<Func<BllRole, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllRole> GetAllByPredicate(Expression<Func<BllRole, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllRole, DalRole>(Expression.Parameter(typeof(DalRole), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalRole, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return roleRepository.GetAllByPredicate(exp).Select(role => role.ToBllRole()).ToList();
        }
    }
}
