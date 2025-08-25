using MyWeb.Data.Models;
public interface IJwtHelper
{
    public string GenerateToken(User user);
}