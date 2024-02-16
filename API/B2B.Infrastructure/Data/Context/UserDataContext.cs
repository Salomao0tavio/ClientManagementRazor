using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B2B.Infrastructure.Data.Context;

public class UserDataContext : IdentityDbContext
{
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options)
    {
    }
}