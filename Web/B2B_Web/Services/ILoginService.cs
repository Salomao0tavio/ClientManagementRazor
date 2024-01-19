using System.Security.Authentication;
using System.Threading.Tasks;
using B2B.Models.Models;

namespace B2B_Web.Services
{
    public interface ILoginService
    {
        Task<string> Login(UserDTO model);
    }

    
}
