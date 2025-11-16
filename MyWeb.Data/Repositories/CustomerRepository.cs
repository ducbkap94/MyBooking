using Microsoft.EntityFrameworkCore;
using MyWeb.Business.Dtos.Customer.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;

namespace MyWeb.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MyWebDbContext _context;   
        public CustomerRepository(MyWebDbContext context)
        {
            _context = context;
        }
        // Implementation of the ICustomer interface methods would go here
        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            return await _context.Customers.AddAsync(customer) != null && await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCustomerAllAsync(List<Customer> customer)
        {
             _context.Customers.RemoveRange(customer);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> DeleteCustomerAsync(Customer customer)
        {
           return Task.Run(async () =>
            {
                _context.Customers.Remove(customer);
                return await _context.SaveChangesAsync() > 0;
            });
        }

        public async Task<List<Customer>> GetAllCustomersAsync(int[] ids)
        {
            return await _context.Customers
                .Where(c => ids.Contains(c.CustomerId))
                .ToListAsync();
        }

        public Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return Task.FromResult(_context.Customers.AsEnumerable());
        }

        public Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return _context.Customers.FirstOrDefaultAsync(a => a.CustomerId == id);
        }

        public Task<CustomerResponse> GetCustomerFormResponseAsync(int? id = null)
        {
            var customer = _context.Customers.Where(x=>x.CustomerId == id).Select(c => new CustomerResponse
            {
                Id = c.CustomerId,
                Name = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                IdentityCard = c.IdentityCard
            }).FirstOrDefault();
            return Task.FromResult(customer);
        }

        public  async Task<PagedResult<CustomerResponse>> GetCustomersPagingAsync(PagingRequest request)
        {
            var query = _context.Customers.Select(c=>new CustomerResponse
            {
                Id = c.CustomerId,
                Name = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                IdentityCard = c.IdentityCard
            }).AsQueryable();

            var totalRecords = await query.CountAsync();

            var items = await query
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<CustomerResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResult<CustomerResponse>> GetCustomersPagingAsync(string searchName, PagingRequest request)
        {
           var query = _context.Customers.Select(c=>new CustomerResponse
            {
                Id = c.CustomerId,
                Name = c.FullName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address,
                IdentityCard = c.IdentityCard
            }).AsQueryable();
            var totalRecords = await query.CountAsync();

            var items = await query.Where(c=>c.Name.Contains(searchName) || c.IdentityCard.Contains(searchName))
                .OrderBy(b => b.Name) // sắp xếp cho ổn định
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedResult<CustomerResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            return await _context.SaveChangesAsync().ContinueWith(t => customer);
        }
    }
}