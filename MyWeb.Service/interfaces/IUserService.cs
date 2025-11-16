using MyWeb.Business.Dtos.Users.Response;
using MyWeb.Business.DTOs.User;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;
namespace MyWeb.Service
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(RegisterUserRequest request);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int userId);
        Task<PagedResult<UserResponse>> GetUsersPaging(PagingRequest request);

        Task<CheckUserResult> IsUserAvailableAsync(RegisterUserRequest request);
        Task<PagedResult<UserResponse>> GetUsersByPagingAsync(string searchName, PagingRequest pagingRequest);
    }
}
