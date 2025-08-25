using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWeb.Data.Models
{
    public class Inventory
    {
        [Key, ForeignKey("Product")]
        public int ProductId { get; set; } // Khóa chính và cũng là FK đến Product

        public Product? Product { get; set; }

        // Số lượng hiện có để cho thuê
        public int QuantityAvailable { get; set; }

        // Số lượng đang cho thuê
        public int QuantityRented { get; set; }

        // Số lượng đang bảo trì
        public int QuantityMaintenance { get; set; }
    }
}
