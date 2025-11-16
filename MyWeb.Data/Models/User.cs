using System.ComponentModel.DataAnnotations;

namespace MyWeb.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        
        public string? Username { get; set; }

        [Required, EmailAddress, MaxLength(150)]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        [MaxLength(150)]
        public string? FullName { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// 0 = Khác, 1 = Nam, 2 = Nữ
        /// </summary>
        public byte? Gender { get; set; }

        [MaxLength(255)]
        public string? Address { get; set; }

        [MaxLength(255)]
        public string? AvatarUrl { get; set; }

        [MaxLength(50)]
        public string? IdentityNumber { get; set; } // CMND/CCCD

        [MaxLength(50)]
        public string? TaxCode { get; set; } // Nhà cung cấp mới có

        [MaxLength(100)]
        public string? BankAccount { get; set; } // Nhà cung cấp mới có
        [MaxLength(100)]
        public string? BankName { get; set; } // Nhà cung cấp mới có
        /// <summary>
        /// 0 = Inactive, 1 = Active, 2 = Blocked
        /// </summary>
        public byte Status { get; set; } 

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
