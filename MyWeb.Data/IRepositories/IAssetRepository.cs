using MyWeb.Data.Models;
using MyWeb.Common.Paging;
using MyWeb.Business.Dtos.Asset.Response;
using MyWeb.Business.Dtos.Asset;
namespace MyWeb.Data.IRepositories
{
    public interface IAssetRepository
    {
        Task<Asset?> GetAssetByIdAsync(int id);
        Task<List<Asset>> GetAllAssetsAsync(int[] ids);
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<bool> CreateAssetAsync(Asset asset);
        Task<Asset> UpdateAssetAsync(Asset asset);
        Task<bool> DeleteAssetAllAsync(List<Asset> asset);
        Task<bool> DeleteAssetAsync(Asset asset);
        Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(PagingRequest request);
        Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(string searchName, PagingRequest request);
        Task<AssetFormResponse> GetAssetFormResponseAsync(int? id = null);
        Task<List<AssetSelectResponse>> GetAllAssetSelectAsync(String? searchName = null, DateTime? fromDate = null, DateTime? toDate = null);
    }
}