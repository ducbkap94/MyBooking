
using MyWeb.Service;
using MyWeb.Data.IRepositories;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly JwtHelper _jwtHelper;

    public AuthService(IUserRepository userRepo, JwtHelper jwtHelper)
    {
        _userRepo = userRepo;
        _jwtHelper = jwtHelper;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        //Gọi Service để xác thực người dùng
        var user = await _userRepo.AuthenticateUserAsync(username);
        //Kiểm tra người dùng có tồn tại và nếu không thì trả về chuỗi rỗng
        if (user == null) return string.Empty;
        //Kiểm tra mật khẩu có hợp lệ hay không
        //Sử dụng BCrypt để so sánh mật khẩu đã mã hóa với mật khẩu người dùng nhập vào
        //Nếu mật khẩu không hợp lệ thì trả về chuỗi rỗng
        var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isPasswordValid) return string.Empty;
        //Nếu mật khẩu hợp lệ, tạo JWT token cho người dùng
        return _jwtHelper.GenerateToken(user);
    }
}