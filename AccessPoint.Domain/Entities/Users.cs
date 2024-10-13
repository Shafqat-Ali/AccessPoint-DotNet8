using System.ComponentModel.DataAnnotations;

namespace AccessPoint.Domain.Entities
{
    public class Users
    {
        [Key]
        public int UserId { get; set; } // Primary key
        public required string Username { get; set; } // Unique username (email)
        public required string PasswordHash { get; set; } // Hashed password
        public required string FirstName { get; set; } // User's first name
        public required string LastName { get; set; } // User's last name
        public string? Device { get; set; } // Device info
        public string? IPAddress { get; set; } // User's IP address
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Account creation date
        public decimal? CurrentBalance { get; set; }

        // Navigation properties can be added later if needed
        public virtual ICollection<LoginHistory> LoginHistory { get; set; }
    }
}
