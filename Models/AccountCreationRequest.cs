using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class AccountCreationRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required] 
        public string StreetName { get; set; }
        [Required]
        public string CivicNumber { get; set; }
        [Required] 
        public string ZipCode { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}