using System.ComponentModel.DataAnnotations;
namespace MyWeb.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string? Username { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? PasswordHash { get; set; }

        public string? FullName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserRole>? UserRoles { get; set; }
    }
    
 }


