using System;
using System.Text.Json.Serialization;
using WebApi.Entities;

namespace WebApi.Models
{
    public class AuthenticateResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            Id = user.Id.ToString();
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            UserType = user.UserType;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}