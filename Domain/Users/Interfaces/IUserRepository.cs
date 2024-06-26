﻿using Domain.Users.Models;
using Infra.Common.Result;

namespace Domain.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<Result<User>> AddAsync(User user, CancellationToken cancellationToken);
        Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken cancellationToken);      
        Task<Result<User>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
