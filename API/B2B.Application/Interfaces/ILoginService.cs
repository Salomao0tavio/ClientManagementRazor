using B2B.Application.DTOs.Request;
using B2B.Application.DTOs.Response;

namespace B2B.Application.Interfaces;

public interface ILoginService
{
    Task<UserLoginResponse> Login(UserLoginRequest userLogin);
}