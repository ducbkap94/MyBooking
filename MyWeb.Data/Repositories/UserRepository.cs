using Microsoft.EntityFrameworkCore;
using MyWeb.Data.Models;
using MyWeb.Business.Dtos;
using MyWeb.Data.IRepositories;
using MyWeb.Business.Request;
using MyWeb.Business.DTOs.User;
using MyWeb.Common.Paging;
using System.Text.Json.Nodes;
using MyWeb.Business.Dtos.Users.Response;
namespace MyWeb.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyWebDbContext _context;

        public UserRepository(MyWebDbContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUserAsync(string username)
        {
            return await  _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);


        }

        public async Task<bool> CreateUserAsync(User user)
        {
            
            _context.Users.Add(user);
            return await _context.SaveChangesAsync() >0;

            
        }

        public Task<bool> DeleteUserAsync(User user)
        {
            if (user.UserRoles != null)
                _context.UserRoles.RemoveRange(user.UserRoles); 
            _context.Users.Remove(user);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedResult<UserResponse>> GetUsersPagingAsync(string searchName, PagingRequest request)
        {
            var query =  _context.Users.Include(u=>u.UserRoles).ThenInclude(ur=>ur.Role).AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
            {
                query = query.Where(b => b.FullName.Contains(searchName));
            }

            var totalRecords = await query.CountAsync();

            var items = await  query
                .OrderBy(b => b.CreatedAt) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize).Select(u=>new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Status = u.Status,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Roles = u.UserRoles != null ? u.UserRoles.Select(ur => ur.Role.Name).ToList() : new List<string>()
                    
                })
                .ToListAsync();

            return new PagedResult<UserResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };


        }

        public  async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u=>u.UserRoles).ThenInclude(ur=>ur.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public  async Task<PagedResult<UserResponse>> GetUsersByPagingAsync(PagingRequest pagingRequest)
        {
            var query = _context.Users.AsQueryable();

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.CreatedAt) // sắp xếp cho ổn định
                .Skip((pagingRequest.Page - 1) * pagingRequest.PageSize)
                .Take(pagingRequest.PageSize).Select(u=>new UserResponse
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Status = u.Status,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    Roles = u.UserRoles != null ? u.UserRoles.Select(ur => ur.Role.Name).ToList() : new List<string>()
                    
                })
                .ToListAsync();
            

            return new PagedResult<UserResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = pagingRequest.Page,
                PageSize = pagingRequest.PageSize
            };
        }

        public async Task<CheckUserResult> IsUserAvailableAsync(RegisterUserRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Username == request.Username))
            {
                return new CheckUserResult
                {
                    Success = false,
                    Message = "Tài khoản đã tồn tại"
                };
            }
            else if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return new CheckUserResult
                {
                    Success = false,
                    Message = "Email đã tồn tại"
                };
            }
            else if (await _context.Users.AnyAsync(u => u.PhoneNumber == request.PhoneNumber))
            {
                return new CheckUserResult
                {
                    Success = false,
                    Message = "Số điện thoại đã tồn tại"
                };
            }
            else if (await _context.Users.AnyAsync(u => u.IdentityNumber == request.IdentityNumber))
            {
                return new CheckUserResult
                {
                    Success = false,
                    Message = "Số CMND/CCCD đã tồn tại"
                };
            }
            return new CheckUserResult
            {
                Success = true,
                Message = "Tài khoản khả dụng"
            };
            
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync().ContinueWith(t => t.Result > 0 ? user : null);
        }
    }
}