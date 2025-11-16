
using MyWeb.Service;
using MyWeb.Data.IRepositories;
using MyWeb.Business.Request;
using MyWeb.Business.Dtos;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly JwtHelper _jwtHelper;

    public AuthService(IUserRepository userRepo, JwtHelper jwtHelper)
    {
        _userRepo = userRepo;
        _jwtHelper = jwtHelper;
    }

    public async Task<UserDto?> GetUserByIdAsync(UserLoginRequest request)
    {
        var user = await _userRepo.AuthenticateUserAsync(request.Username);
        //Kiểm tra người dùng có tồn tại và nếu không thì trả về chuỗi rỗng
        if (user == null) return null;
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid) return null;
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
        };
        return userDto;
    }

    public async Task<string?> LoginAsync(UserLoginRequest request)
    {
        //Gọi Service để xác thực người dùng
        var user = await _userRepo.AuthenticateUserAsync(request.Username);
        //Kiểm tra người dùng có tồn tại và nếu không thì trả về chuỗi rỗng
        if (user == null) return string.Empty;
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
        if (!isPasswordValid) return string.Empty;
        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
        };
        //Nếu mật khẩu hợp lệ, tạo JWT token cho người dùng
        return _jwtHelper.GenerateToken(userDto);
    }
}