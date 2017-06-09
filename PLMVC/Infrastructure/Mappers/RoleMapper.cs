using BLL.Interface.Entities;
using PLMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMVC.Infrastructure.Mappers
{
    public static class RoleMapper
    {
        public static RoleModel ToMvcRole(this BllRole bllRole)
        {
            return new RoleModel()
            {
                Id = bllRole.Id,
                Name = bllRole.Name
            };
        }

    }
}