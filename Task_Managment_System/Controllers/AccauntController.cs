using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Task_Managment_System.Server.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Abstraction;
using Dtos;
using Task_Managment_System.Core.Models;
using Core.Responses;
using Microsoft.AspNetCore.Http;

namespace Task_Managment_System.Server.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccauntController : ControllerBase
    {
        private readonly IIdentityService _service;
        private readonly ILogger<AccauntController> _logger;
        private readonly CurrentUserSupplier _currentUserSupplier;

        public AccauntController(CurrentUserSupplier currentUserSupplier, IIdentityService service,
            ILogger<AccauntController> logger)
        {


            this._service = service;

            this._logger = logger;

            this._currentUserSupplier = currentUserSupplier;
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody]UserWrite User)
        {


            _logger.LogInformation("Registration", User);


            return Ok(await _service.Registration(User, User.Password));

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthentificationResult))]
        public async Task<ActionResult<AuthentificationResult>> Login([FromBody]LoginDto User)
        {
            _logger.LogInformation("Login", User);


            var result = await _service.Login(User);

            if (result.Success)
            {
                return Ok(result);
            }

            return Unauthorized();

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete()
        {
            var curUser = await _currentUserSupplier.GetCurrentUser();

            _logger.LogInformation("Delete", User);

            return Ok(await _service.Delete(curUser.Id));

        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserRead userDto)
        {


            _logger.LogInformation("Update", User);



            return Ok(await _service.Update(userDto));

        }


    }
}
