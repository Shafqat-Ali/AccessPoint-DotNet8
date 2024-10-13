using System.ComponentModel.DataAnnotations;

namespace AccessPoint.Domain.Entities
{
    public class LoginHistory
    {
        [Key]
        public int LoginHistoryId { get; set; } // Primary key
        public int UserId { get; set; } // Foreign key referencing User
        public DateTime LoginDateTime { get; set; } = DateTime.UtcNow; // Time of login
        public required string IPAddress { get; set; } // IP address used during login
        public required string Device { get; set; } // Device information
        public required string Browser { get; set; } // Flag to indicate first login

        // Navigation property
        public virtual Users? User { get; set; } // Navigation property to User
    }
}
