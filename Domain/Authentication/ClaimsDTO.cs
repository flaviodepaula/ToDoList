using System.ComponentModel.DataAnnotations;

namespace Domain.Authentication
{
    public class ClaimsDTO
    {
        public string Email { get; set; }
        public string Role { get; set; }

        public ClaimsDTO(string email, string role)
        {
            Email = email;
            Role = role;
        }
    };
}
