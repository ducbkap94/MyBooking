using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MyWeb.Data.Models;
public class MyWebDbContext : DbContext
{
    public MyWebDbContext(DbContextOptions<MyWebDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    // ====== Catalog ======
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    // ====== Inventory ======
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }

    // ====== Booking ======
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<BookingDetail> BookingDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }

    // ====== Finance ======
    public DbSet<CashBook> CashBooks { get; set; }

    // ====== Marketing ======
    public DbSet<AdvertisingCost> AdvertisingCosts { get; set; }
    // ====== Brand ======
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<RentalRate> RentalRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // ===== User - Role =====
        modelBuilder.Entity<UserRole>()
        .HasKey(ur => new { ur.UserId, ur.RoleId }); // 👈 composite key
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Assets)
            .WithOne(a => a.Product)
            .HasForeignKey(a => a.ProductId);




        modelBuilder.Entity<Inventory>()
            .Property(p => p.QuantityAvailable)
            .HasDefaultValue(0);

        modelBuilder.Entity<Maintenance>()
            .Property(m => m.Cost)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Payment>()
            .Property(p => p.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<CashBook>()
            .Property(c => c.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<AdvertisingCost>()
            .Property(a => a.Amount)
            .HasColumnType("decimal(18,2)");



        // ===== Quan hệ 1-n: Product - Maintenance =====

        // ===== Quan hệ Booking - BookingDetail =====
        modelBuilder.Entity<Booking>()
            .HasMany(b => b.BookingDetails)
            .WithOne(d => d.Booking)
            .HasForeignKey(d => d.BookingId);

        // ===== Quan hệ Booking - Payment =====
        modelBuilder.Entity<Booking>()
            .HasMany(b => b.Payments)
            .WithOne(p => p.Booking)
            .HasForeignKey(p => p.BookingId);

        // ===== Quan hệ CashBook - Payment / AdvertisingCost =====
        modelBuilder.Entity<CashBook>()
            .HasOne(c => c.Payment)
            .WithOne(p => p.CashBook)
            .HasForeignKey<Payment>(p => p.CashBookId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<CashBook>()
            .HasOne(c => c.AdvertisingCost)
            .WithOne(a => a.CashBook)
            .HasForeignKey<AdvertisingCost>(a => a.CashBookId)
            .OnDelete(DeleteBehavior.SetNull);
        // ===== Quan hệ 1-n: Brand - Product =====
        modelBuilder.Entity<Brand>().ToTable("Brands").HasKey(b => b.Id);
        modelBuilder.Entity<Product>().HasOne(p => p.Brand).WithMany(b => b.Products).HasForeignKey(p => p.BrandId);
        // ===== Thiết lập giá trị mặc định và dữ liệu khởi tạo cho User, Role, UserRole =====
        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
        modelBuilder.Entity<User>()
            .Property(u => u.UpdatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
        modelBuilder.Entity<Asset>()
            .Property(x => x.PurchasePrice)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Booking>()
            .Property(x => x.TotalAmount)
            .HasPrecision(18, 2);
        modelBuilder.Entity<BookingDetail>()
            .Property(x => x.PricePerUnit)
            .HasPrecision(18, 2);
        modelBuilder.Entity<RentalRate>()
            .Property(x => x.PricePerDay)
            .HasPrecision(18, 2);

        modelBuilder.Entity<RentalRate>()
            .Property(x => x.PricePerHour)
            .HasPrecision(18, 2);
        modelBuilder.Entity<Booking>()
            .HasMany(b => b.BookingDetails)
            .WithOne(d => d.Booking)
            .HasForeignKey(d => d.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = "$2a$11$SP9uoiu9At/n1f9dUSdguuIt6o.ScgEUhAIgmWkuqFKOQNKs3SVrq",
            Email = "ducbkap94@gmail.com",
            FullName = "Lương Đức",
        }, new User
        {
            Id = 2,
            Username = "admin1",
            PasswordHash = "$2a$11$SP9uoiu9At/n1f9dUSdguuIt6o.ScgEUhAIgmWkuqFKOQNKs3SVrq",
            Email = "abc@gmail.com",
            FullName = "Lương Minh",
        }
                );
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" },
            new Role { Id = 3, Name = "Supplier" }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 2, RoleId = 2 } // Admin  
        );
    }

}



