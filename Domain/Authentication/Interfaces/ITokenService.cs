using Domain.Authentication.DTO;
using Domain.Users.Models;
using Infra.Common.Result;

namespace Domain.Authentication.Interfaces
{
    public interface ITokenService
    {
        Task<Result<string>> GenerateTokenAsync(LoginDto userLogin, CancellationToken cancellationToken);
    }
}
