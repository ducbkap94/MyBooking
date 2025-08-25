using MyWeb.Data.Models;
namespace MyWeb.Service
{
    public interface IUserService
{
    Task<User> GetUserByIdAsync(int userId);

}
}
