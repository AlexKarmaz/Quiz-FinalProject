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
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
        }

        public IEnumerable<BllProfile> GetAll()
        {
            return profileRepository.GetAll().Select(u => u.ToBllProfile());
        }

        public BllProfile GetById(int id)
        {
            var profile = profileRepository.GetById(id);
            if (profile == null)
                return null;
            return profile.ToBllProfile();
        }

        public void Create(BllProfile entity)
        {
            profileRepository.Create(entity.ToDalProfile());
            unitOfWork.Commit();
        }

        public void Delete(BllProfile entity)
        {
            profileRepository.Delete(entity.ToDalProfile());
            unitOfWork.Commit();
        }

        public void Update(BllProfile entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserId(BllProfile entity, int id)
        {
            profileRepository.UpdateUserId(entity.ToDalProfile(), id);
            unitOfWork.Commit();
        }

        public BllProfile GetOneByPredicate(Expression<Func<BllProfile, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        public IEnumerable<BllProfile> GetAllByPredicate(Expression<Func<BllProfile, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllProfile, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return profileRepository.GetAllByPredicate(exp).Select(profile => profile.ToBllProfile()).ToList();
        }
    }
}
