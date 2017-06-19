using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface IProfileService : IService<BllProfile>
    {
        /// <summary>
        /// Updates the user's id on the profile
        /// </summary>
        /// <param name="entity">Profile</param>
        /// <param name="id">User id</param>
        void UpdateUserId(BllProfile entity, int id);
        /// <summary>
        /// Deletes references to a profile in the test
        /// </summary>
        /// <param name="userId">User id</param>
        void DeleteTestReference(int userId);
    }
}
