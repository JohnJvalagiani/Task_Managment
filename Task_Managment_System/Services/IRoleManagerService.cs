using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_Managment_System.Server.Models;

namespace Server.Services.Abstraction
{
   public interface IRoleManagerService
    {
        DisplayRolesModel GetAllRules();
        Task<IdentityResult> CreateRole(RoleModel role);
        Task AssigneRole(AssigneRole assigne);
        Task UpdateUserRole(UpdateUserRoleModel updateUserRole);
        Task<bool> DeleteRole(UpdateUserRoleModel updateUserRole);
    }
}
