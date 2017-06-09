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
    public class ThemeService : IThemeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IThemeRepository themeRepository;

        public ThemeService(IUnitOfWork unitOfWork, IThemeRepository themeRepository)
        {
            this.unitOfWork = unitOfWork;
            this.themeRepository = themeRepository;
        }

        public IEnumerable<BllTheme> GetAll()
        {
            return themeRepository.GetAll().Select(u => u.ToBllTheme());
        }

        public BllTheme GetById(int id)
        {
            var theme = themeRepository.GetById(id);
            if (theme == null)
                return null;
            return theme.ToBllTheme();
        }

        public void Create(BllTheme entity)
        {
            themeRepository.Create(entity.ToDalTheme());
            unitOfWork.Commit();
        }

        public void Delete(BllTheme entity)
        {
            themeRepository.Delete(entity.ToDalTheme());
            unitOfWork.Commit();
        }

        public void Update(BllTheme entity)
        {
            themeRepository.Update(entity.ToDalTheme());
            unitOfWork.Commit();
        }

        public BllTheme GetOneByPredicate(Expression<Func<BllTheme, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllTheme> GetAllByPredicate(Expression<Func<BllTheme, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllTheme, DalTheme>(Expression.Parameter(typeof(DalTheme), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalTheme, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return themeRepository.GetAllByPredicate(exp).Select(theme => theme.ToBllTheme()).ToList();
        }
    }
}
