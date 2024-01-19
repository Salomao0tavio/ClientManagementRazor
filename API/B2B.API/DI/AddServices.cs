using Infrastructure.B2B.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Application.B2B.Aplication.Interfaces.Services;
using Services.B2B.Identity.Services;

namespace Application.B2B.Application
{
    public static class AddServices
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDbContext<IdentityDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IIdentityService, IdentityService>();
        }

    }
}
