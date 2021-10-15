
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Task_Managment_System.Core.Models;

namespace Task_Managment_System.Server.Services
{
    public class CurrentUserSupplier
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserSupplier(  IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                return null;
            }

            var emailClaim = user.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));

            if (emailClaim is null)
            {
                return null;
            }

            var email = emailClaim.Value;

            var userManager = _httpContextAccessor.HttpContext.RequestServices
                .GetService<UserManager<AppUser>>();

            return await userManager.FindByEmailAsync(email);
        }

    }
}
