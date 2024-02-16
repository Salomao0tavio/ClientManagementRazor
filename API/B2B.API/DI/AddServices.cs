using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using B2B.Infrastructure.Data.Context;
using B2B.Infrastructure.Data.Repositories;
using B2B.Application.Interfaces;
using B2B.Application.Services;

namespace B2B.API.DI
{
    public static class AddServices
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<UserDataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserDataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

    }
}
