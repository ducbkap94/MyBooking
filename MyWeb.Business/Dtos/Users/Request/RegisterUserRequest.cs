using System;
using System.ComponentModel.DataAnnotations;

namespace MyWeb.Business.DTOs.User
{
    public class RegisterUserRequest
    {
     
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        

    
        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;


        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }

        public int Gender { get; set; } 
        public string? Address { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();
        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }
        public string? IdentityNumber { get; set; } // CMND/CCCD
        public int Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string? TaxCode { get; set; } // Nhà cung cấp mới có
        public string? BankAccount { get; set; } // Nhà cung cấp mới có
        public string? BankName { get; set; } // Nhà cung cấp mới có

        [Url]
        public string? AvatarUrl { get; set; }
    }
}
