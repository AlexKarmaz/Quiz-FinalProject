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
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.Repositories
{
    public class ThemeRepository : IThemeRepository
    {
        private readonly DbContext context;

        public ThemeRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalTheme> GetAll()
        {
            var themes = context.Set<Theme>().Select(theme => theme).ToList();
            return themes.Select(u => u.ToDalTheme()).ToList();
        }

        public DalTheme GetById(int id)
        {
            var ormTheme = context.Set<Theme>().Find(id);
            return ormTheme.ToDalTheme();
        }

        public void Create(DalTheme entity)
        {
            var theme = entity.ToOrmTheme();
            context.Set<Theme>().Add(theme);
        }

        public void Delete(DalTheme entity)
        {
            var ormTheme = entity.ToOrmTheme();
            var theme = context.Set<Theme>().FirstOrDefault(u => u.Id == ormTheme.Id);
            context.Set<Theme>().Attach(theme);
            context.Set<Theme>().Remove(theme);
            context.Entry(theme).State = System.Data.Entity.EntityState.Deleted;
        }

        public void Update(DalTheme entity)
        {
            if (entity != null)
            {
                var themeToUpdate = context.Set<Theme>().FirstOrDefault(u => u.Id == entity.Id);
                var ormTheme = entity.ToOrmTheme();
                context.Set<Theme>().Attach(themeToUpdate);
                themeToUpdate.Name = ormTheme.Name;
                context.Entry(themeToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public DalTheme GetOneByPredicate(Expression<Func<DalTheme, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public IEnumerable<DalTheme> GetAllByPredicate(Expression<Func<DalTheme, bool>> predicate)
        {
            var visitor = new PredicateExpressionVisitor<DalTheme, Theme>(Expression.Parameter(typeof(Theme), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<Theme, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = context.Set<Theme>().Where(express).Select(u => u).ToList();
            return final.Select(u => u.ToDalTheme()).ToList();
        }
    }
}
