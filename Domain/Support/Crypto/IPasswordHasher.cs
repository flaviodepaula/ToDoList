﻿namespace Domain.Support.Crypto
{
    public interface IPasswordHasher
    {
        bool VerifyPassword(string password, string hashedPassword);
        string HashPassword(string password);
        
    }
}
