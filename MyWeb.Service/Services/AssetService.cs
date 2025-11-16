using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using MyWeb.Business.Dtos.Asset;
using MyWeb.Business.Dtos.Asset.Response;
using MyWeb.Common.Paging;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;
using MyWeb.Service.Interfaces;

namespace MyWeb.Service.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;
        public AssetService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<JsonResult> CreateAssetAsync(AssetRequest asset)
        {
            var lstasset = await _assetRepository.GetAllAssetsAsync();
            if (lstasset.Any(a => a.SerialNumber == asset.SerialNumber))
            {
                return new JsonResult(new { Success = false, Message = "Serial Number đã tồn tại." });
            }
            var result = await _assetRepository.CreateAssetAsync(new Asset
            {
                AssetId = asset.AssetId,
                ProductId = asset.ProductId,
                SupplierId = asset.SupplierId,
                PurchaseDate = asset.PurchaseDate,
                WarrantyPeriod = asset.WarrantyPeriod,
                Status = asset.Status,
                SerialNumber = asset.SerialNumber,
                PurchasePrice = asset.PurchasePrice,
                Note = asset.Note,

            });
            if (result)
            {
                return new JsonResult(new { Success = true, Message = "Thêm mới thành công." });
            }
            return new JsonResult(new { Success = false, Message = "Thêm mới thất bại." });

        }

        public Task<bool> DeleteAssetAllAsync(List<Asset> assets)
        {
            return _assetRepository.DeleteAssetAllAsync(assets);
        }

        public Task<bool> DeleteAssetAsync(Asset asset)
        {
            return _assetRepository.DeleteAssetAsync(asset);
        }

        public Task<List<Asset>> GetAllAssetsAsync(int[] ids)
        {
            return _assetRepository.GetAllAssetsAsync(ids);
        }

        public Task<Asset> GetAssetByIdAsync(int id)
        {
            return _assetRepository.GetAssetByIdAsync(id).ContinueWith(t => t.Result!);
        }

        public Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(PagingRequest request)
        {
            return _assetRepository.GetAssetsPagingAsync(request);
        }

        public Task<PagedResult<AssetTableResponse>> GetAssetsPagingAsync(string searchName, PagingRequest request)
        {
            return _assetRepository.GetAssetsPagingAsync(searchName, request);
        }

        public Task<JsonResult> UpdateAssetAsync(AssetRequest request)
        {
            var asset = _assetRepository.GetAssetByIdAsync(request.AssetId).Result;
            if (asset == null)
            {
                return Task.FromResult(new JsonResult(new { Success = false, Message = "Thiết bị không tồn tại." }));
            }

    

            asset.WarrantyPeriod = request.WarrantyPeriod;
            asset.ProductId = request.ProductId;
            asset.SupplierId = request.SupplierId;
            asset.Status = request.Status;
            asset.SerialNumber = request.SerialNumber;
            asset.PurchasePrice = request.PurchasePrice;
            asset.Note = request.Note;
            asset.OwnerId = request.OwnerId;
            var rs = _assetRepository.UpdateAssetAsync(asset).ContinueWith(t => t.Result != null);
            if (rs.Result)
            {
                return Task.FromResult(new JsonResult(new { Success = true, Message = "Cập nhật thành công." }));
            }
            return Task.FromResult(new JsonResult(new { Success = false, Message = "Cập nhật thất bại." }));
        }

        public Task<AssetFormResponse> GetAssetFormResponseAsync(int? id = null)
        {
            return _assetRepository.GetAssetFormResponseAsync(id);
        }

        public Task<List<AssetSelectResponse>> GetAllAssetSelectAsync(string? searchName = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            return _assetRepository.GetAllAssetSelectAsync(searchName, fromDate, toDate);
        }
    }
}