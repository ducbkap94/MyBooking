using MyWeb.Business.Dtos.Product;
using MyWeb.Business.Dtos.RentalRate;

namespace MyWeb.Business.Dtos.RentalRate.Response
{
    public class RentalRateTableResponse
    {
        public int RateId { get; set; }
        public String ProductName { get; set; }
        public decimal PricePerDay { get; set; }
        public decimal PricePerHour { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }


}
    