using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Server.Services.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;
using Task_Managment_System.Server.Models;

namespace Roles.Controllers
{
    [Authorize( Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleManagerService _roleManager;

        public RolesController(IRoleManagerService roleManager)
        {

            _roleManager = roleManager;
        }

        [HttpGet("GetAlloRles")]
        public async Task<IActionResult> GetAllRules()
        {
           
            return Ok(_roleManager.GetAllRules());
        }

      
        [HttpPost("Create role")]
        public async Task<IActionResult> CreateRole(RoleModel roleModel)
        {
            var result = await _roleManager.CreateRole(roleModel);

            return Ok(result);
        }

        [HttpPost("Assigne role")]
        public async Task<IActionResult> AssigneRole(AssigneRole assigneRole)
        {
            await _roleManager.AssigneRole(assigneRole);

            return Ok();
        }

        [HttpDelete("Remove role")]
        public async Task<IActionResult> RemoveUserRole(UpdateUserRoleModel updateUserRole)
        {
            await _roleManager.DeleteRole(updateUserRole);

            return Ok();
        }

        [HttpPut("Update user role")]
        public async Task<IActionResult> UpdateUserRole(UpdateUserRoleModel updateUserRole)
        {
            await  _roleManager.UpdateUserRole(updateUserRole);

            return Ok();
        }
    }

   

}
