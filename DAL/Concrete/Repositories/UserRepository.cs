﻿using DAL.Interface;
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
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext context)
        {
            this.context = context;
        }
        public string[] GetRolesForUser(string userName)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.UserName == userName);
            int i = 0;
            string[] roles = new string[user.Roles.Count];
           
            foreach(Role role in user.Roles)
            {
                roles[i] = role.Name;
                i++;
            }

            return roles;
        }
        public IEnumerable<DalUser> GetAll()
        {
            var users = context.Set<User>().Select(user => user).ToList();
            return users.Select(u => u.ToDalUser()).ToList();
        }

        public DalUser GetById(int id)
        {
            var ormUser = context.Set<User>().Find(id);
            return ormUser.ToDalUser();
        }

        public void Create(DalUser entity, int roleId)
        {
            var role = context.Set<Role>().FirstOrDefault(r => r.Id == roleId);
            var profile = context.Set<Profile>().FirstOrDefault(p => p.Id == entity.ProfileId);
            var user = entity.ToOrmUser();

            if (role != null)
                user.Roles.Add(role);

            if (profile != null)
                user.Profile = profile;

            context.Set<User>().Add(user);
        }

        public void Delete(DalUser entity)
        {
            var ormUser = entity.ToOrmUser();
            var user = context.Set<User>().FirstOrDefault(u => u.Id == ormUser.Id);

            context.Set<User>().Attach(user);
            context.Set<User>().Remove(user);
            context.Entry(user).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalUser entity)
        {
            if (entity != null)
            {
                var userToUpdate = context.Set<User>().FirstOrDefault(u => u.Id == entity.Id);
                var ormUser = entity.ToOrmUser();
                context.Set<User>().Attach(userToUpdate);
                userToUpdate.UserName = ormUser.UserName;
                userToUpdate.Email = ormUser.Email;
                userToUpdate.Password = ormUser.Password;
                userToUpdate.Roles = ormUser.Roles;
                context.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public DalUser GetOneByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalUser> GetAllByPredicate(Expression<Func<DalUser, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalUser, User>(Expression.Parameter(typeof(User), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<User, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<User>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalUser()).ToList();
        }
    }
}
