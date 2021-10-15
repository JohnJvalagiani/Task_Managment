using Task_Managment_System.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Responses;
using Dtos;

namespace Task_Managment_System.Abstraction
{
   public  interface IIdentityService 
    {
       Task<AuthentificationResult> Registration(UserWrite user, string password);
       Task<AuthentificationResult> Login(LoginDto loginDto);
       Task<AuthentificationResult> RefreshTokenAsync(string token,string  refreshToken);
       Task<bool> Update(UserRead appUser);
       Task<bool> Delete(string id);
    }
}
