namespace MyWeb.Business.Dtos.Booking.Request
{
    public class BookingDetailRequest
    {
        public int AssetId { get; set; }
        public decimal Price { get; set; }
        public int Days { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public string RateType { get; set; }
    

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

    }
}