using MyWeb.Business.Dtos;
using MyWeb.Business.Request;

namespace MyWeb.Service
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(UserLoginRequest request);
        Task<UserDto?> GetUserByIdAsync(UserLoginRequest request);
    }
}