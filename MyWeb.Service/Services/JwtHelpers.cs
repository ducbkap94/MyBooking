using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyWeb.Business.Dtos;
public class JwtHelper
{

  
    private readonly JwtSettings _jwtSettings;

    public JwtHelper(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }


    public string GenerateToken(UserDto user)
{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim("Id", user.Id.ToString()),
        new Claim("UserName", user.Username),
        new Claim("Email", user.Email ?? ""),
        new Claim("FullName", user.FullName ?? "")
    };
    if (user != null)
    {
        foreach (var userRole in user.Roles)
        {
            claims = claims.Append(new Claim(ClaimTypes.Role, userRole ?? "")).ToArray();
            Console.WriteLine("Role added to token: " + userRole);
        }
    }

    var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Audience,
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
        signingCredentials: creds);
    Console.WriteLine("Token generated for user: " + new JwtSecurityTokenHandler().WriteToken(token));
    return new JwtSecurityTokenHandler().WriteToken(token);
}
}
