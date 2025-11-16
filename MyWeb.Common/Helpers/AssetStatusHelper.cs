namespace MyWeb.Common.Helpers
{
    public static class AssetStatusHelper
    {
        public static string GetStatusName(int status)
        {
            return status switch
            {
                1 => "Sẵn sàng",
                2 => "Đang thuê",
                3 => "Bảo trì",
                _ => "Không xác định",
            };
        }
    }
}