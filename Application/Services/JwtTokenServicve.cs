using Core.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace Core.Services
{
    public class JWTTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public JWTTokenService(JwtSettings jwtSettings,IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            this._jwtSettings = jwtSettings;

        }

        public async Task<AuthentificationResult> BuildToken(UserInfoDTO userInfoDTO)
        {

            var identityUser = await _userManager.FindByEmailAsync(userInfoDTO.EmailAddress);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userInfoDTO.EmailAddress),
            };

            var userClaims = await _userManager.GetClaimsAsync(identityUser);

            claims.AddRange(userClaims);

            var JWTKey = Encoding.UTF8.GetBytes(_jwtSettings.Secret);

            var securityKey = new SymmetricSecurityKey(JWTKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddDays(7);

            JwtSecurityToken JWTToken =
                new JwtSecurityToken(
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: expirationDate,
                    signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(JWTToken);

            return new AuthentificationResult(token, expirationDate,true);
        }
    }
}
