using System.Security.AccessControl;
using MyWeb.Business.Dtos.Customer.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;

namespace MyWeb.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomersAsync(int[] ids);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAllAsync(List<Customer> customer);
        Task<bool> DeleteCustomerAsync(Customer customer);
        Task<PagedResult<CustomerResponse>> GetCustomersPagingAsync(PagingRequest request);
        Task<PagedResult<CustomerResponse>> GetCustomersPagingAsync(string searchName, PagingRequest request);
        Task<CustomerResponse> GetCustomerFormResponseAsync(int? id = null);
    }
}