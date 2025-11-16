using Microsoft.AspNetCore.Identity;
using MyWeb.Business.Dtos;
using MyWeb.Business.Dtos.Users.Response;
using MyWeb.Business.DTOs.User;
using MyWeb.Business.Request;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;
namespace MyWeb.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string username);
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<PagedResult<UserResponse>> GetUsersByPagingAsync(PagingRequest pagingRequest);
        Task<PagedResult<UserResponse>> GetUsersPagingAsync(string searchName, PagingRequest request);
        Task<CheckUserResult> IsUserAvailableAsync(RegisterUserRequest request);

        }
}
