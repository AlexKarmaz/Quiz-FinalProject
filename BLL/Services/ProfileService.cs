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
    /// Realization of IProfileService interface.
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProfileRepository profileRepository;

        public ProfileService(IUnitOfWork unitOfWork, IProfileRepository profileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.profileRepository = profileRepository;
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection of profiles</returns>
        public IEnumerable<BllProfile> GetAll()
        {
            return profileRepository.GetAll().Select(u => u.ToBllProfile());
        }

        /// <summary>
        /// Gets a profile by id
        /// </summary>
        /// <param name="id">Profile id</param>
        /// <returns>Profile</returns>
        public BllProfile GetById(int id)
        {
            var profile = profileRepository.GetById(id);
            if (profile == null)
                return null;
            return profile.ToBllProfile();
        }

        /// <summary>
        /// Creates a profile
        /// </summary>
        /// <param name="entity">Profile</param>
        public void Create(BllProfile entity)
        {
            profileRepository.Create(entity.ToDalProfile());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a profile
        /// </summary>
        /// <param name="entity">Profile</param>
        public void Delete(BllProfile entity)
        {
            profileRepository.Delete(entity.ToDalProfile());
            unitOfWork.Commit();
        }

        /// <summary>
        /// Updates a profile
        /// </summary>
        /// <param name="entity">Profile</param>
        public void Update(BllProfile entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the user's id on the profile
        /// </summary>
        /// <param name="entity">Profile</param>
        /// <param name="id">User id</param>
        public void UpdateUserId(BllProfile entity, int id)
        {
            profileRepository.UpdateUserId(entity.ToDalProfile(), id);
            unitOfWork.Commit();
        }
        /// <summary>
        /// Gets one profile by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Profile</returns>
        public BllProfile GetOneByPredicate(Expression<Func<BllProfile, bool>> predicates)
        {
            return GetAllByPredicate(predicates).FirstOrDefault();
        }

        /// <summary>
        /// Gets all profiles by the predicate
        /// </summary>
        /// <param name="predicates">Predicate</param>
        /// <returns>Collection of profiles</returns>
        public IEnumerable<BllProfile> GetAllByPredicate(Expression<Func<BllProfile, bool>> predicates)
        {
            var visitor = new PredicateExpressionVisitor<BllProfile, DalProfile>(Expression.Parameter(typeof(DalProfile), predicates.Parameters[0].Name));
            var exp = Expression.Lambda<Func<DalProfile, bool>>(visitor.Visit(predicates.Body), visitor.NewParameter);
            return profileRepository.GetAllByPredicate(exp).Select(profile => profile.ToBllProfile()).ToList();
        }

        /// <summary>
        /// Deletes references to a profile in the test
        /// </summary>
        /// <param name="userId">User id</param>
        public void DeleteTestReference(int userId)
        {
            profileRepository.DeleteTestReference(userId);
        }
    }
}
