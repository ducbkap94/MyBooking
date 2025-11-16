namespace MyWeb.Business.Dtos.Booking.Response
{
    public class BookingResponse
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}