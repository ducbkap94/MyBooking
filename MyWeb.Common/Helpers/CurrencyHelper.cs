using System.Globalization;

namespace MyWeb.Common.Helpers
{
    public static class CurrencyHelper
    {
        /// <summary>
        /// Định dạng số tiền theo tiền Việt Nam (VD: 1.500.000 ₫)
        /// </summary>
        public static string FormatVND(decimal amount)
        {
            CultureInfo viVN = new CultureInfo("vi-VN");
            return string.Format(viVN, "{0:C0}", amount); 
            // C0 = không có phần thập phân (VD: 1.000 ₫)
        }

        /// <summary>
        /// Định dạng có phần thập phân (VD: 1.500.000,50 ₫)
        /// </summary>
        public static string FormatVNDWithDecimal(decimal amount)
        {
            CultureInfo viVN = new CultureInfo("vi-VN");
            return string.Format(viVN, "{0:C2}", amount); 
            // C2 = có 2 chữ số thập phân
        }
    }
}
