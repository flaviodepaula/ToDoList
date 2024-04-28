using Domain.Authentication.DTO;
using Domain.Authentication.Interfaces;
using Domain.Crypto;
using Domain.Support;
using Domain.Users.Errors;
using Domain.Users.Interfaces;
using Infra.Common.Result;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Domain.Authentication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly JwtConfig _jwtConfig;

        public TokenService(IUserRepository userRepository,
                            IPasswordHasher passwordHasher,
                            IOptions<JwtConfig> jwtConfig)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<Result<string>> GenerateTokenAsync(LoginDto userLogin, CancellationToken cancellationToken)
        {
            try
            {
                var userData = await _userRepository.GetByEmailAsync(userLogin.Email, cancellationToken);
                if (userData.IsFailure)
                    return Result.Failure<string>(userData.Error);

                var isValidPassword = _passwordHasher.VerifyPassword(userLogin.Password, userData.Value.Password);
                if (!isValidPassword)
                    return Result.Failure<string>(UserDomainErrors.InvalidConfigurations);

                if(!ValidateConfiguration())
                    return Result.Failure<string>(UserDomainErrors.InvalidPassword);

                var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtConfig.Key ?? ""));
                var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                                            issuer: _jwtConfig.Issuer,
                                            audience: _jwtConfig.Audience,
                                            claims: new[]
                                            {
                                            new Claim(type: ClaimTypes.Email, userData.Value.Email),
                                            new Claim(type: ClaimTypes.Role, userData.Value.Role),
                                            },
                                            expires: DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes),
                                            signingCredentials: signInCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return Result.Sucess(token);
            }
            catch (Exception ex)
            {
                return Result.Failure<string>(UserDomainErrors.ErrorOnTokenGeneration(ex.Message, ex.InnerException?.ToString() ?? ""));
            }
        }

        private bool ValidateConfiguration()
        {
            if (_jwtConfig == null) return false;

            if(_jwtConfig.Issuer.IsNullOrEmpty()) return false;

            if(_jwtConfig.Audience.IsNullOrEmpty()) return false;

            if( _jwtConfig.Key.IsNullOrEmpty()) return false;

            return true;
        }
    }
}
