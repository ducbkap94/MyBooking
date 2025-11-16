namespace MyWeb.Business.Dtos.Booking.Request
{
    public class BookingRequest
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<BookingDetailRequest> Items { get; set; }
    }
}