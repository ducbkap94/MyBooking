using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.Asset;
using MyWeb.Business.Dtos.Asset.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.Models;

namespace MyWeb.Service.Interfaces
{
    public interface IAssetService
    {
        Task<Asset> GetAssetByIdAsync(int id);
        Task<List<Asset>> GetAllAssetsAsync(int[] ids); 
        Task<JsonResult> CreateAssetAsync(AssetRequest asset);
        Task<JsonResult> UpdateAssetAsync(AssetRequest asset);
        Task<bool> DeleteAssetAllAsync(List<Asset> assets);
        Task<bool> DeleteAssetAsync(Asset asset);
        Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(PagingRequest request);
        Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(string searchName, PagingRequest request);
        Task<AssetFormResponse> GetAssetFormResponseAsync(int? id = null);
        Task<List<AssetSelectResponse>> GetAllAssetSelectAsync(String? searchName = null, DateTime? fromDate = null, DateTime? toDate = null);
    }
}