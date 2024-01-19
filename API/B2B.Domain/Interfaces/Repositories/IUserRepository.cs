using Domain.B2B.Domain.Entities;

namespace Domain.B2B.Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetUser(string email, string password);
       
    }

}
