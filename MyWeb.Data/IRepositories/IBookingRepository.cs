namespace MyWeb.Data.IRepositories
{
    using MyWeb.Business.Dtos.Booking.Request;
    using MyWeb.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookingRepository
    {
        Task<Booking> GetBookingByIdAsync(int bookingId);
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<bool> CreateBookingAsync(BookingRequest request);
        Task<bool> UpdateBookingAsync(BookingRequest request);
        Task DeleteBookingAsync(int bookingId);
    }
}