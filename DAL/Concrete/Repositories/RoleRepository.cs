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
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext context)
        {
            this.context = context;
        }

        public bool IsUserInRole(string userName, string roleName)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.UserName == userName);

            return user.Roles.Any(r => r.Name == roleName);
        }
        public void Create(DalRole entity)
        {
            context.Set<Role>().Add(entity.ToOrmRole());
        }

        public void Delete(DalRole entity)
        {
            throw new NotImplementedException();
        }

        public void Update(DalRole entity)
        {
            if (entity != null)
            {
                var roleToUpdate = context.Set<Role>().FirstOrDefault(u => u.Id == entity.Id);
                var ormRole = entity.ToOrmRole();
                context.Set<Role>().Attach(roleToUpdate);
                roleToUpdate.Users = ormRole.Users;
                context.Entry(roleToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public IEnumerable<DalRole> GetAll()
        {
            var roles = context.Set<Role>().ToList();
            return roles.Select(r => r.ToDalRole()).ToList();
        }

        public DalRole GetById(int id)
        {
            return context.Set<Role>().Where(r => r.Id == id).FirstOrDefault().ToDalRole();
        }

        public DalRole GetOneByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalRole> GetAllByPredicate(Expression<Func<DalRole, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalRole, Role>(Expression.Parameter(typeof(Role), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Role, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<Role>().Where(express).ToList();
            return final.Select(r => r.ToDalRole());
        }
    }
}
