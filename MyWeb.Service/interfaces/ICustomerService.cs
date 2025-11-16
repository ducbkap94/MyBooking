
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.Booking.Request;
using MyWeb.Business.Dtos.Customer.Request;
using MyWeb.Business.Dtos.Customer.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;
using MyWeb.Data.Repositories;

namespace MyWeb.Service.Interfaces
{
    public interface ICustomerService
    {
      Task<Customer> GetCustomerByIdAsync(int id);
        Task<List<Customer>> GetAllCustomersAsync(int[] ids); 
        Task<JsonResult> CreateCustomerAsync(CustomerRequest request);
        Task<JsonResult> UpdateCustomerAsync(CustomerRequest request);
        Task<bool> DeleteCustomerAllAsync(List<Customer> requests);
        Task<bool> DeleteCustomerAsync(Customer customer);
        Task<PagedResult<CustomerResponse>> GetCustomerPagingAsync(PagingRequest request);
    Task<PagedResult<CustomerResponse>> GetCustomerPagingAsync(string searchName, PagingRequest request);  
 
        Task<CustomerResponse> GetCustomerFormResponseAsync(int? id = null);
    }
}