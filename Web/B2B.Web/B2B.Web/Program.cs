using B2B.Application.Interfaces;
using B2B.Application.Services;
using B2B.Infrastructure.Data.Context;
using B2B.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var services = builder.Services;

services.AddHttpClient<CustomerService>("CustomerServiceClient", client => { client.BaseAddress = new Uri("https://localhost:7063"); });
services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
services.AddDbContext<UserDataContext>(options => options.UseSqlServer(connectionString));
services.AddDefaultIdentity<IdentityUser>(options =>{ options.SignIn.RequireConfirmedAccount = true; }).AddEntityFrameworkStores<UserDataContext>();
services.AddAuthentication().AddGoogle(options =>
{
    var googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");
    options.ClientId = googleAuthNSection["ClientId"] ?? throw new InvalidOperationException();
    options.ClientSecret = googleAuthNSection["ClientSecret"] ?? throw new InvalidOperationException();
});
services.AddHttpClient();
services.AddRazorPages();


services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<CustomerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();