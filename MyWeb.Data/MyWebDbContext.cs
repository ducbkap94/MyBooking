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
        // ===== Decimal precision cho các trường tiền tệ =====
        modelBuilder.Entity<Product>()
            .Property(p => p.PricePerDay)
            .HasColumnType("decimal(18,2)");

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

        // ===== Quan hệ 1-1: Product - Inventory =====
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Inventory)
            .WithOne(i => i.Product)
            .HasForeignKey<Inventory>(i => i.ProductId);

        // ===== Quan hệ 1-n: Product - Maintenance =====
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Maintenances)
            .WithOne(m => m.Product)
            .HasForeignKey(m => m.ProductId);

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
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Username = "admin",
            PasswordHash = "$2a$11$SP9uoiu9At/n1f9dUSdguuIt6o.ScgEUhAIgmWkuqFKOQNKs3SVrq",
            Email = "ducbkap94@gmail.com",
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            FullName = "Lương Đức",
        }, new User
        {
            Id = 2,
            Username = "admin1",
            PasswordHash = "$2a$11$SP9uoiu9At/n1f9dUSdguuIt6o.ScgEUhAIgmWkuqFKOQNKs3SVrq",
            Email = "abc@gmail.com",
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            FullName = "Lương Minh",
        }
                );
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = 1, RoleId = 1 },
            new UserRole { UserId = 2, RoleId = 2 } // Admin  
        );
    }
    
    }
    
    

