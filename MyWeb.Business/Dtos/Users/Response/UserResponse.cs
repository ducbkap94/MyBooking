using System;
using System.Collections.Generic;

namespace MyWeb.Business.DTOs.User
{
    public class UserResponse
    {
        public int Id { get; set; }
        
        public string Username { get; set; }

        public string? FullName { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        // Danh sách role (Admin, Supplier, Customer…)
        public List<string> Roles { get; set; } = new();

        // Trạng thái tài khoản
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
