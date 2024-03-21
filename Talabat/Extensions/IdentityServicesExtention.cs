using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Talabat.Core.IServices;
using Talabat.Core.Models.Identity;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.Extensions
{
    public static class IdentityServicesExtention
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddIdentity<AppUser, IdentityRole>(Options =>
            {
                Options.Password.RequireUppercase = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireDigit = true;       
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            return services;
        }
    }
}
