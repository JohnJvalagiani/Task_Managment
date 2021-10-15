using Core.Responses;
using Core.Services;
using Core.Services.Abstraction;
using Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Task_Managment_System.Server.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task_Managment_System.Abstraction;
using Task_Managment_System.Core.Models;
using AutoMapper;

namespace Task_Managment_System.Implementation
{
    public class IdentityService : IIdentityService
    {
        private readonly IMapper _mapper;
        public readonly CurrentUserSupplier _currentUserSupplier;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTTokenService _jWT;
        private readonly TokenValidationParameters _tokenValidationPrametrs;

        private readonly JwtSettings _jwtSettings;

        public IdentityService(IMapper mapper ,CurrentUserSupplier currentUserSupplier,JWTTokenService jWt
            ,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager /*JwtSettings jwtSettings*/)
        {

            _mapper = mapper;
            this._currentUserSupplier = currentUserSupplier;
            this._jWT = jWt;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        public async Task<AuthentificationResult> Registration(UserWrite user, string password)
        {
            var theuser = await _userManager.FindByEmailAsync(user.Email);

            if (theuser != null)
                return new AuthentificationResult
                {
                    Errors = new[] { "User with this email adress already exists" }
                };


           var newUser = _mapper.Map<AppUser>(user);

            newUser.UserName = user.Email;

            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                return new AuthentificationResult
                {
                    Errors = result.Errors.Select(e => e.Description)

                };
            }

          

       

              return await _jWT.BuildToken(new UserInfoDTO { EmailAddress = user.Email });

        }


        public async Task<AuthentificationResult> Login(LoginDto user)
        {

            var theuser = await _userManager.FindByEmailAsync(user.Email);

             if (theuser == null)
                return new AuthentificationResult
                {
                    Errors = new string[] { $"Password or Email not correct" }
                };
         
             
          var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false,false);

            if(result.Succeeded)
            return await _jWT.BuildToken(new UserInfoDTO { EmailAddress = theuser.Email });


            return new AuthentificationResult
            {
                Errors = new string[] { $"Password or Email not correct" }
            };

        }


        public async Task<bool> Update(UserRead appUser)
        {


            var curUser = await _currentUserSupplier.GetCurrentUser();

            curUser.Email = appUser.Email;
            curUser.FirstName = appUser.Name;

            var result=await _userManager.UpdateAsync(curUser);

            return result.Succeeded;
           
        }

        public async Task<bool> Delete(string id)
        {
           var theuser=await  _userManager.FindByIdAsync(id);
           
           var result= await _userManager.DeleteAsync(theuser);

            return result.Succeeded;
        
        }
        
        private async Task<AuthentificationResult> GenerateAuthenicationResultForUser(AppUser theuser)
        {
           return  await _jWT.BuildToken(new UserInfoDTO {EmailAddress=theuser.Email});

           
        }

        public async Task<AuthentificationResult> RefreshTokenAsync(string token, string refreshToken)
        {

            var validateToken = GetPrincipalFromToken(token);

            if(validateToken==null)
            {
                return new AuthentificationResult {Errors=new[] { "Invalid Token"} };
            
            }

            var expiryDateUnix=long.Parse(validateToken.Claims.Single(x=>x.Type==JwtRegisteredClaimNames.Exp).Value);


            var expireDateTimeutc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);


            if (expireDateTimeutc>DateTime.Now)
            {
                return new AuthentificationResult { Errors = new[] { "This Token Hasnt Expirer yet" } };

            }
            


            return null;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationPrametrs, out var validationToken);

                if (!IsJwtWithSecurityAlgorithm(validationToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }


            
        }

        private bool IsJwtWithSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtsecurityToken)
                && jwtsecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);


        }

    }
}
