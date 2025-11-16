namespace MyWeb.Business.Dtos.RentalRate
{
    public class RentalRateDto
    {
        public int? RateId { get; set; }
        public int? ProductId { get; set; }

        public decimal? PricePerDay { get; set; }
        public decimal? PricePerHour { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

}