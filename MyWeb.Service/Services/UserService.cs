using MyWeb.Business.Dtos.Users.Response;
using MyWeb.Business.DTOs.User;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;

namespace MyWeb.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUserAsync(RegisterUserRequest request)
        {
            if (request.Id == 0 || request.Id == null)
            {
                var user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    FullName = request.FullName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password), // Lưu mật khẩu đã băm
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    DateOfBirth = request.DateOfBirth,
                    // Assuming Gender is a byte? in RegisterUserRequest
                    Gender = (byte)request.Gender,
                    IdentityNumber = request.IdentityNumber,
                    Status = (byte)request.Status,
                    TaxCode = request.TaxCode,
                    BankAccount = request.BankAccount,
                    BankName = request.BankName,
                    AvatarUrl = request.AvatarUrl,
                    UserRoles = request.Roles?.Select(roleId => new UserRole { RoleId = int.Parse(roleId) }).ToList(),

                };

                return await _userRepository.CreateUserAsync(user);
            }
            else
            {
                var user = _userRepository.GetUserByIdAsync(request.Id).Result;
                user.Username = request.Username;
                user.Email = request.Email;
                user.FullName = request.FullName;
                if (!string.IsNullOrEmpty(request.Password))
                {
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password); // Cập nhật mật khẩu nếu có
                }
                user.PhoneNumber = request.PhoneNumber;
                user.Address = request.Address;
                user.DateOfBirth = request.DateOfBirth;
                user.Gender = (byte)request.Gender;
                user.IdentityNumber = request.IdentityNumber;
                user.Status = (byte)request.Status;
                user.TaxCode = request.TaxCode;
                user.BankAccount = request.BankAccount;
                user.BankName = request.BankName;
                user.AvatarUrl = request.AvatarUrl;
                // Cập nhật vai trò
                user.UserRoles = request.Roles?.Select(roleId => new UserRole { RoleId = int.Parse(roleId), UserId = user.Id }).ToList();
                return await _userRepository.UpdateUserAsync(user) != null;


            }
        }

        public Task<bool> DeleteUserAsync(int userId)
        {
            return _userRepository.GetUserByIdAsync(userId).ContinueWith(t =>
            {
                var user = t.Result;
                if (user == null)
                {
                    return Task.FromResult(false);
                }
                return _userRepository.DeleteUserAsync(user);
            }).Unwrap();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<UserResponse>> GetUsersByPagingAsync(string searchName, PagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest), "Paging request cannot be null");
            }

            return _userRepository.GetUsersPagingAsync(searchName, pagingRequest);
        }

        public Task<User> GetUserByIdAsync(int userId)
        {
            return _userRepository.GetUserByIdAsync(userId);
        }

        public Task<PagedResult<UserResponse>> GetUsersPaging(PagingRequest request)
        {
             return _userRepository.GetUsersByPagingAsync(request);
        }

        public Task<CheckUserResult> IsUserAvailableAsync(RegisterUserRequest request)
        {
            return _userRepository.IsUserAvailableAsync(request);
        }

        public Task<User> UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}