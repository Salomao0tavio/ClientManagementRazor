using Domain.B2B.Domain.Entities;
using Infrastructure.B2B.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Domain.B2B.Domain.Repositories;

namespace Infrastructure.B2B.Infrastructure.Data.Repositories
{
    public class UserRepository(DataContext context) : IUserRepository
    {
        private readonly DataContext _context = context;

        public async Task<User> GetUser(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }
    }
}