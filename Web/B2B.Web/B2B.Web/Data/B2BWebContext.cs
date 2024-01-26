using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace B2B.Web.Data;

public class B2BWebContext : IdentityDbContext<IdentityUser>
{
    public B2BWebContext(DbContextOptions<B2BWebContext> options)
        : base(options)
    {
    }
}
