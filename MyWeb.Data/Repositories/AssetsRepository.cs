using Azure;
using Microsoft.EntityFrameworkCore;
using MyWeb.Business.Dtos;
using MyWeb.Business.Dtos.Asset;
using MyWeb.Business.Dtos.Asset.Response;
using MyWeb.Business.Dtos.Brand;
using MyWeb.Business.Dtos.Product;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;

namespace MyWeb.Data.Repositories
{
    public class AssetsRepository : IAssetRepository
    {
        private readonly MyWebDbContext _context;
        public AssetsRepository(MyWebDbContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateAssetAsync(Asset asset)
        {
            return await _context.Assets.AddAsync(asset) != null && await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> DeleteAssetAllAsync(List<Asset> assets)
        {
            _context.Assets.RemoveRange(assets);
            return _context.SaveChangesAsync().ContinueWith(t => t.Result > 0);
        }

        public async Task<bool> DeleteAssetAsync(Asset asset)
        {
            _context.Assets.Remove(asset);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<List<Asset>> GetAllAssetsAsync(int[] ids)
        {
            return _context.Assets
                .Where(a => ids.Contains(a.AssetId))
                .ToListAsync();
        }

        public Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return Task.FromResult(_context.Assets.AsEnumerable());
        }

        public Task<List<AssetSelectResponse>> GetAllAssetSelectAsync(String? searchName = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            var query = from a in _context.Assets
                        join p in _context.Products on a.ProductId equals p.ProductId
                        join r in _context.RentalRates on a.ProductId equals r.ProductId
                        select new AssetSelectResponse
                        {
                            Id = a.AssetId,
                            ProductName = p.Name,
                            SerialNumber = a.SerialNumber,
                            PricePerDay = r.PricePerDay,
                            PricePerHour = r.PricePerHour==null ? 0 : r.PricePerHour.Value,
                        };
            return query.Where(c =>
                    (c.ProductName.Contains(searchName) || c.SerialNumber.Contains(searchName)) &&
                    !_context.BookingDetails.Any(bd => bd.AssetId==c.Id && bd.StartDate <= toDate && bd.EndDate >= fromDate)
                    ).ToListAsync();

        }

        public Task<Asset?> GetAssetByIdAsync(int id)
        {
            return _context.Assets.FirstOrDefaultAsync(a => a.AssetId == id);
        }

        public Task<AssetFormResponse> GetAssetFormResponseAsync(int? id = null)
        {

            var lstur = from u in _context.Users
                        join ur in _context.UserRoles on u.Id equals ur.UserId
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where r.Name == "Supplier"
                        select new UserDto
                        {
                            Id = u.Id,
                            FullName = u.FullName
                        };

            var response = new AssetFormResponse
            {
                Products = _context.Products.Select(prod => new ProductDto
                {
                    Id = prod.ProductId,
                    Name = prod.Name
                }).ToList(),
                Suppliers = lstur.ToList(),
                Asset = _context.Assets.Where(a => a.AssetId == id).Select(a => new AssetDto
                {
                    AssetId = a.AssetId,
                    ProductId = a.ProductId,
                    SerialNumber = a.SerialNumber,
                    SupplierId = a.SupplierId,
                    PurchasePrice = a.PurchasePrice,
                    PurchaseDate = a.PurchaseDate,
                    WarrantyPeriod = a.WarrantyPeriod,
                    Status = a.Status
                }).FirstOrDefault()

            };

            return Task.FromResult(response);

        }

        public async Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(PagingRequest request)
        {
            var query = from a in _context.Assets
                        join p in _context.Products on a.ProductId equals p.ProductId
                        join u in _context.Users on a.SupplierId equals u.Id
                        select new AssetTableResponse
                        {
                            Id = a.AssetId,
                            ProductName = p.Name,
                            SerialNumber = a.SerialNumber,
                            Supplier = u.FullName,
                            PurchasePrice = a.PurchasePrice,
                            PurchaseDate = a.PurchaseDate.HasValue ? a.PurchaseDate.Value.ToString("dd/MM/yyyy") : null,
                            WarrantyPeriod = a.WarrantyPeriod,

                            Status = a.Status,

                        };
            var asset = await query.AsQueryable().ToListAsync();

            var totalRecords = await query.CountAsync();

            var items = await query
            .OrderBy(b => b.ProductName) // sắp xếp cho ổn định
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

            return new PagedResult<AssetTableResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(string searchName, PagingRequest request)
        {
            var query = from a in _context.Assets
                        join p in _context.Products on a.ProductId equals p.ProductId
                        join u in _context.Users on a.SupplierId equals u.Id
                        select new AssetTableResponse
                        {
                            Id = a.AssetId,
                            ProductName = p.Name,
                            SerialNumber = a.SerialNumber,
                            Supplier = u.FullName,
                            PurchasePrice = a.PurchasePrice,
                            PurchaseDate = a.PurchaseDate.HasValue ? a.PurchaseDate.Value.ToString("yyyy-MM-dd") : null,
                            WarrantyPeriod = a.WarrantyPeriod,

                            Status = a.Status,

                        };
            var asset = await query.AsQueryable().ToListAsync();

            var totalRecords = await query.CountAsync();

            var items = await query
            .OrderBy(b => b.ProductName).Where(a => a.ProductName != null && a.ProductName.Contains(searchName) || a.SerialNumber != null && a.SerialNumber.Contains(searchName))// sắp xếp cho ổn định
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();
            return new PagedResult<AssetTableResponse>
            {
                Items = items,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize
            };
        }

        public async Task<Asset> UpdateAssetAsync(Asset asset)
        {
            _context.Assets.Update(asset);
            return await _context.SaveChangesAsync().ContinueWith(t => asset);
        }
    }
}