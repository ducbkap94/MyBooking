using MyWeb.Data.Models;
namespace MyWeb.Data.IRepositories
{
    public interface IUserRepository
        {
         Task<User>  AuthenticateUserAsync(string username);
        }
}
