namespace MyWeb.Data.Repositories
{
    using MyWeb.Data.IRepositories;
    using MyWeb.Data.Models;
    using MyWeb.Common.Paging;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MyWeb.Business.Dtos.RentalRate;
    using MyWeb.Business.Dtos.Product;
    using MyWeb.Business.Dtos.RentalRate.Response;

    public class RateRepository : IRateRepository
    {
        private readonly MyWebDbContext _context;

        public RateRepository(MyWebDbContext context)
        {
            _context = context;
        }

        public Task<bool> CreateRateAsync(RentalRate rentalRate)
        {
            _context.RentalRates.Add(rentalRate);
            return _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
        }

        public Task<bool> DeleteRateAllAsync(List<RentalRate> rentalRate)
        {
            _context.RentalRates.RemoveRange(rentalRate);
            return _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
        }

        public Task<bool> DeleteRateAsync(RentalRate assrentalRateet)
        {
            _context.RentalRates.Remove(assrentalRateet);
            return _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
        }

        public async Task<List<RentalRate>> GetAllRateAsync(int[] ids)
        {
            return await  _context.RentalRates.Where(r => ids.Contains(r.RateId)).ToListAsync();
        }

        public Task<IEnumerable<RentalRate>> GetAllRateAsync()
        {
            return Task.FromResult(_context.RentalRates.AsEnumerable());
        }

        public Task<RentalRate?> GetRateByIdAsync(int id)
        {
            return _context.RentalRates.FirstOrDefaultAsync(r => r.RateId == id);
        }
        //Lấy dữ liệu lên form
        public Task<RentalRateFromResponse> GetRateFormResponseAsync(int? id = null)
        {
            var lstproducts = _context.Products
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name
                }).ToListAsync();
            var response = new RentalRateFromResponse
            {
                Products = lstproducts.Result,

                RentalRate =  _context.RentalRates
                    .Where(r => r.RateId == id)
                    .Select(r => new RentalRateDto
                    {
                        RateId = r.RateId,
                        ProductId = r.ProductId,
                        PricePerDay = r.PricePerDay,
                        PricePerHour = r.PricePerHour,
                        StartDate = r.StartDate,
                        EndDate = r.EndDate 
                    }).FirstOrDefault()
            };
            return Task.FromResult(response);
            
        }

        public async Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(PagingRequest request)
        {
            var query = from r in _context.RentalRates
                        join p in _context.Products on r.ProductId equals p.ProductId
                        select new RentalRateTableResponse
                        {
                            RateId = r.RateId,
                            ProductName = p.Name,
                            PricePerDay = r.PricePerDay,
                            PricePerHour = r.PricePerHour== null ? 0 : r.PricePerHour.Value,
                            StartDate = r.StartDate,
                            EndDate = r.EndDate
                        };
            var rate= await query.AsQueryable().ToListAsync();

            var totalRecords = await query.CountAsync();

            var items = await query
            .OrderBy(b => b.ProductName) // sắp xếp cho ổn định
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            return new PagedResult<RentalRateTableResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResult<RentalRateTableResponse>> GetRatePagingAsync(string searchName, PagingRequest request)
        {
            var query = from r in _context.RentalRates
                        join p in _context.Products on r.ProductId equals p.ProductId where p.Name.Contains(searchName)
                        select new RentalRateTableResponse
                        {
                            RateId = r.RateId,
                            ProductName = p.Name,
                            PricePerDay = r.PricePerDay,
                            PricePerHour = r.PricePerHour== null ? 0 : r.PricePerHour.Value,
                            StartDate = r.StartDate,
                            EndDate = r.EndDate
                        };
            var rate= await query.AsQueryable().ToListAsync();

            var totalRecords = await query.CountAsync();

            var items = await query
            .OrderBy(b => b.ProductName) // sắp xếp cho ổn định
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            return new PagedResult<RentalRateTableResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<RentalRate> UpdateRateAsync(RentalRate rentalRate)
        {
            _context.RentalRates.Update(rentalRate);
            return await  _context.SaveChangesAsync().ContinueWith(t => rentalRate);
        }
    }
}

       