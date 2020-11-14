using WebApi.Entities;

namespace WebApi.Models
{
    public class AccountCreationResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AccountCreationResponse(User user)
        {
            Email = user.Email;
            Password = user.Password;
        }
    }
}