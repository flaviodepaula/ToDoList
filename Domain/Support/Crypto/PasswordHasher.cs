using BCrypt.Net;

namespace Domain.Support.Crypto
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int maxLenght = 10;
         
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(maxLenght));
        }
        
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

}
