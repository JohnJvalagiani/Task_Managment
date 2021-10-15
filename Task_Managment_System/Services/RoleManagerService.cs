using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Server.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;
using Task_Managment_System.Server.Models;

namespace Task_Managment_System.Server.Services
{
    public class RoleManagerService : IRoleManagerService
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleManagerService(ApplicationDbContext context,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }
        public async Task AssigneRole(AssigneRole assigneRole)
        {
            var user = await _userManager.FindByEmailAsync(assigneRole.UserEmail);

            await _userManager.AddToRoleAsync(user, assigneRole.Role);

        }

        public async Task<IdentityResult> CreateRole(RoleModel roleModel)
        {
          var result = await _roleManager.CreateAsync(new IdentityRole { Name = roleModel.Name });

            return result;
        }

        public async Task<bool> DeleteRole(UpdateUserRoleModel updateUserRole)
        {
            var user = await _userManager.FindByEmailAsync(updateUserRole.UserEmail);

            if (user == null)
                throw new Exception($"User with email {updateUserRole.UserEmail}  Not Found");

            await _userManager.RemoveFromRoleAsync(user, updateUserRole.Role);

            return true;
        }

        public DisplayRolesModel GetAllRules()
        {
            var roles = _context.Roles.ToList();
            var users = _context.Users.ToList();
            var userRoles = _context.UserRoles.ToList();

            var convertedUsers = users.Select(x => new UsersModel
            {
                Email = x.Email,
                Roles = roles
                    .Where(y => userRoles.Any(z => z.UserId == x.Id && z.RoleId == y.Id))
                    .Select(y => new UsersRole
                    {
                        Name = y.NormalizedName
                    })
            });

         return   new DisplayRolesModel
            {
                Roles = roles.Select(x => x.NormalizedName),
                Users = convertedUsers
            };
        }

        public async Task UpdateUserRole(UpdateUserRoleModel updateUserRole)
        {
            var user = await _userManager.FindByEmailAsync(updateUserRole.UserEmail);

          
                await _userManager.AddToRoleAsync(user, updateUserRole.Role);
        }
    }
}
