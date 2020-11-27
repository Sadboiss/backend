using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApi.Entities;

namespace WebApi.Controllers.DTOs
{
    public class UserDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserType { get; set; }
        [Required]
        public List<RefreshToken> RefreshTokens { get; set; }
        [Required]
        public virtual Address Address { get; set; }
    }
}