using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessPoint.Application.DTOs
{
    public class UserAuthDto
    {
        [EmailAddress]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]//
        public required string IPAddress { get; set; }
        [Required]
        public required string Device { get; set; }
        [Required]
        public required string Browser { get; set; }
    }
    public class UserAuthResponseDto
    {
        public required string firstname { get; set; }
        public required string lastname { get; set; }
        public required string token { get; set; }
    }
}
