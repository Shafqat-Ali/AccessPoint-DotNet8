using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.DTOs
{
    public class UserSignUpDto
    {
        [Required]
        [EmailAddress]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Device { get; set; }
        [Required]
        public required string IPAddress { get; set; }
    }
}
