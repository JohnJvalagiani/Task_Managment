using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Managment_System.Abstraction;
using Task_Managment_System.Core.Models;
using Task_Managment_System.Implementation;

namespace Task_Managment_System.Server.Installers
{
    public class DbInstaller : IInstaller
    {
      
        public void InstallerService(IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IIdentityService, IdentityService>();



            services.AddDbContext<ApplicationDbContext>
                   (opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));



          

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
               .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));

                options.AddPolicy("ManagerAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole("Admin")
                                || context.User.IsInRole("Manager")));

                options.AddPolicy("UserAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole("Admin")
                                || context.User.IsInRole("Manager")
                                || context.User.IsInRole("User")));
            });

        }
    }
}
