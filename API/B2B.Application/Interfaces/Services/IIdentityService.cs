using Application.B2B_Application.DTOs.Request;
using Application.B2B_Application.DTOs.Response;

namespace Application.B2B.Aplication.Interfaces.Services
{
    public interface IIdentityService
    {
        Task<UserLoginResponse> Login(UserLoginRequest userLogin);
    }

}
