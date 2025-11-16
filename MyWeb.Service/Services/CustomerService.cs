namespace MyWeb.Service.Services
{
    using Microsoft.AspNetCore.Mvc;

    using MyWeb.Business.Dtos.Customer.Request;
    using MyWeb.Business.Dtos.Customer.Response;
    using MyWeb.Common.Paging;
    using MyWeb.Data.Models;
    using MyWeb.Data.Repositories;
    using MyWeb.Service.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task<List<Customer>> GetAllCustomersAsync(int[] ids)
        {
            return await _customerRepository.GetAllCustomersAsync(ids);
        }

        public async Task<JsonResult> CreateCustomerAsync(CustomerRequest request)
        {
            var lstCus = await _customerRepository.GetAllCustomersAsync();
            var checkIdentityCard = lstCus.FirstOrDefault(c => c.IdentityCard == request.IdentityCard);
            var checkEmail = lstCus.FirstOrDefault(c => c.Email == request.Email);
            var checkPhone = lstCus.FirstOrDefault(c => c.Phone == request.Phone);
            if (checkEmail!=null)
            {
                return new JsonResult(new { success = false, message = "Email đã có người sử dụng." });
            }
            else if (checkPhone != null)
            {
                return new JsonResult(new { success = false, message = "Số điện thoại đã có người sử dụng." });
            }
            else if (checkIdentityCard != null)
            {
                return new JsonResult(new { success = false, message = "Số CMND/CCCD đã có người sử dụng." });
            }
            
            var customer = new Customer
            {
                CustomerId = request.Id,
                FullName = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                IdentityCard = request.IdentityCard

            };

            var result = await _customerRepository.CreateCustomerAsync(customer);
            if (result)
            {
                return new JsonResult(new { success = true, message = "Thêm khách hàng thành công" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Có lỗi xảy ra." });
            }
        }

        public async Task<JsonResult> UpdateCustomerAsync(CustomerRequest request)
        {
            var customer = _customerRepository.GetCustomerByIdAsync(request.Id).Result;
            var lstCus = await _customerRepository.GetAllCustomersAsync();
            var checkIdentityCard = lstCus.FirstOrDefault(c => c.IdentityCard == request.IdentityCard);
            var checkEmail = lstCus.FirstOrDefault(c => c.Email == request.Email);
            var checkPhone = lstCus.FirstOrDefault(c => c.Phone == request.Phone);
            if (checkEmail!=null)
            {
                return new JsonResult(new { success = false, message = "Email đã có người sử dụng." });
            }
            else if (checkPhone != null)
            {
                return new JsonResult(new { success = false, message = "Số điện thoại đã có người sử dụng." });
            }
            else if (checkIdentityCard != null)
            {
                return new JsonResult(new { success = false, message = "Số CMND/CCCD đã có người sử dụng." });
            }
            if (customer == null)
            {
                return await Task.FromResult(new JsonResult(new { success = false, message = "Khách hàng không tồn tại." }));
            }
            
            customer.FullName = request.Name;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.Address = request.Address;
            customer.IdentityCard = request.IdentityCard;
            var rs= _customerRepository.UpdateCustomerAsync(customer).ContinueWith(t => t.Result != null);
            if (rs.Result)
            {
                return await Task.FromResult(new JsonResult(new { success = true, message = "Cập nhật khách hàng thành công." }));
            }
            return await Task.FromResult(new JsonResult(new { success = false, message = "Cập nhật khách hàng thất bại." }));
        }

        public async Task<bool> DeleteCustomerAllAsync(List<Customer> requests)
        {
            
            return await _customerRepository.DeleteCustomerAllAsync(requests);
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            return await _customerRepository.DeleteCustomerAsync(customer);
        }

        public async Task<PagedResult<CustomerResponse>> GetCustomerPagingAsync(PagingRequest request)
        {
            return await _customerRepository.GetCustomersPagingAsync(request);
        }

        public async Task<PagedResult<CustomerResponse>> GetCustomerPagingAsync(string searchName, PagingRequest request)
        {
            return await _customerRepository.GetCustomersPagingAsync(searchName, request);
        }

        public Task<CustomerResponse> GetCustomerFormResponseAsync(int? id = null)
        {
            return _customerRepository.GetCustomerFormResponseAsync(id);
        }
    }
}