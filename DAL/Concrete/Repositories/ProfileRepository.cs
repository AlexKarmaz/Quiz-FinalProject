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
    public class ProfileRepository : IProfileRepository
    {
        private readonly DbContext context;

        public ProfileRepository(DbContext context)
        {
            this.context = context;
        }
        public void Create(DalProfile entity)
        {
            context.Set<Profile>().Add(entity.ToOrmProfile());
        }

        public void Delete(DalProfile entity)
        {
            var userProfile = context.Set<Profile>().FirstOrDefault(p => p.Id == entity.Id);
            context.Set<Profile>().Attach(userProfile);
            context.Set<Profile>().Remove(userProfile);
            context.Entry(userProfile).State = System.Data.Entity.EntityState.Deleted;
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return context.Set<Profile>().Select(p => p.ToDalProfile());
        }

        public IEnumerable<DalProfile> GetAllByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalProfile, Profile>(Expression.Parameter(typeof(Profile), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Profile, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<Profile>().Where(express).ToList();
            return final.Select(p => p.ToDalProfile());
        }

        public DalProfile GetById(int id)
        {
            var profile = context.Set<Profile>().Find(id);
            if (profile == null)
                return null;
            return profile.ToDalProfile();
        }

        public DalProfile GetOneByPredicate(Expression<Func<DalProfile, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public void Update(DalProfile entity)
        {
            throw new NotImplementedException();
            //if (entity != null)
            //{
            //    var profile = context.Set<Profile>().FirstOrDefault(p => p.Id == entity.Id);
            //    if (profile != null)
            //    {
            //        context.Set<Profile>().Attach(profile);
            //        var ormProfile = entity.ToOrmProfile();

            //        //profile.FirstName = e.FirstName ?? profile.FirstName;
            //        //profile.LastName = e.LastName ?? profile.LastName;

            //        //profile.City = e.City ?? profile.City;

            //        context.Entry(profile).State = EntityState.Modified;
            //    }
            //}

        }

    }
}
