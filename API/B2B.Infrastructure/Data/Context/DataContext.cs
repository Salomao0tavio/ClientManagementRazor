using Domain.B2B.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.B2B.Infrastructure.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

    }
}