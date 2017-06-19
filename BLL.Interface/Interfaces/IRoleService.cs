using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Interfaces
{
    public interface IRoleService : IService<BllRole>
    {
        /// <summary>
        /// Verifies that the user belongs to the role
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="roleName">Rolename</param>
        /// <returns>bool value</returns>
        bool IsUserInRole(string userName, string roleName);
    }
}
