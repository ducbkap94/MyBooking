using Microsoft.EntityFrameworkCore;
using MyWeb.Business.Dtos.Booking.Request;
using MyWeb.Data.IRepositories;
using MyWeb.Data.Models;

namespace MyWeb.Data.Repositories
{

    public class BookingRepository : IBookingRepository
    {
        private readonly MyWebDbContext _context;
        public BookingRepository(MyWebDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateBookingAsync(BookingRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var booking = new Booking
                {
                    CustomerId = request.CustomerId,
                   
                    Status = 1,
                    CreatedAt = DateTime.Now
                };
                await _context.Bookings.AddAsync(booking);
                await _context.SaveChangesAsync();
                decimal totalAmount = 0;
                foreach (var detail in request.Items)
                {
                    var rate = _context.RentalRates.FirstOrDefault(r => r.AssetId == detail.AssetId && r.EndDate >= DateTime.Now && r.StartDate <= DateTime.Now);
                    if (rate == null)
                    {
                        throw new Exception($"Không tìm thấy bảng giá cho tài sản {detail.AssetId}");
                    }
                    decimal price = detail.RateType == "Hour"
                        ? (rate?.PricePerHour ?? 0m)
                        : (rate?.PricePerDay ?? 0m);
                    var bookingDetail = new BookingDetail
                    {
                        BookingId = booking.BookingId,
                        AssetId = detail.AssetId,
                        PricePerUnit = price,
                        CreatedAt = DateTime.Now,
                        TotalAmount = detail.Days * price * (1 - detail.Discount / 100),
                        Days = detail.Days,
                        Discount = detail.Discount,
                        RateType = detail.RateType,

                    };
                    totalAmount += bookingDetail.TotalAmount;

                    _context.BookingDetails.Add(bookingDetail);

                }
                booking.TotalAmount = totalAmount;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                transaction.Rollback();
                return false;
            }

        }

        public Task DeleteBookingAsync(int bookingId)
        {
            _context.Bookings.Remove(new Booking { BookingId = bookingId });
            return _context.SaveChangesAsync();
        }

        public Task<IEnumerable<Booking>> GetAllBookingsAsync()
        {
            return Task.FromResult(_context.Bookings.AsEnumerable());
        }

        public  Task<Booking> GetBookingByIdAsync(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            return  Task.FromResult(booking);
        }

        public async Task<bool> UpdateBookingAsync(BookingRequest request)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var booking = await _context.Bookings
                    .FirstOrDefaultAsync(b => b.BookingId == request.BookingId);

                if (booking == null)
                    throw new Exception("Không tìm thấy booking");

                // Update booking info
                booking.CustomerId = request.CustomerId;

                // Xóa detail cũ
                var oldDetails = _context.BookingDetails
                    .Where(d => d.BookingId == request.BookingId);
                _context.BookingDetails.RemoveRange(oldDetails);
                await _context.SaveChangesAsync();

                // Thêm detail mới
                decimal totalAmount = 0;

                foreach (var detail in request.Items)
                {
                    var rate = await _context.RentalRates
                        .FirstOrDefaultAsync(r =>
                            r.AssetId == detail.AssetId &&
                            r.EndDate >= DateTime.Now &&
                            r.StartDate <= DateTime.Now);

                    if (rate == null)
                        throw new Exception($"Không tìm thấy bảng giá cho thiết bị {detail.AssetId}");

                    decimal price = detail.RateType == "Hour"
                        ? (rate?.PricePerHour ?? 0m)
                        : (rate?.PricePerDay ?? 0m);

                    var newDetail = new BookingDetail
                    {
                        BookingId = booking.BookingId,
                        AssetId = detail.AssetId,
                        Days = detail.Days,
                        Discount = detail.Discount,
                        PricePerUnit = price,
                        TotalAmount = detail.Days * price * (1 - detail.Discount / 100),
                        RateType = detail.RateType,
                        CreatedAt = DateTime.Now
                    };

                    totalAmount += newDetail.TotalAmount;
                    await _context.BookingDetails.AddAsync(newDetail);
                }

                booking.TotalAmount = totalAmount;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}