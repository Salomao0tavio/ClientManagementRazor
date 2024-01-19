
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.B2B.Infrastructure.Data.Context
{
    public class IdentityDataContext : IdentityDbContext
    {

        public IdentityDataContext(DbContextOptions<IdentityDataContext> options) : base(options) { }

    }
}
