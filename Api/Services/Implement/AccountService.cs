using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Api.Constants;
using Api.Repository.Interface;

namespace Api.Services.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ICenterRepository _centerRepository;
        private readonly IConfiguration _configuration;
        
        public AccountService(IUserRepository userRepository, ICenterRepository centerRepository, ITokenRepository tokenRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _centerRepository = centerRepository;
            _configuration = configuration;
        }
        
        public async Task RegisterAccountAsync(RegisterAccountDTO register)
        {
            var center = new Center
            {
                Name = $"Center_{register.Username}_{DateTime.UtcNow}",
                Address = null,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                ManagerId = 1,
                CreatedBy = DefaultValues.SystemId,
                UpdatedBy = DefaultValues.SystemId,
            };
            await _centerRepository.AddCenterAsync(center);
            
            var user = new User
            {
                Username = register.Username,
                FullName = register.FullName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password),
                RoleId = DefaultValues.StudentRoleId,
                CenterId = center.Id,
                LastModifiedTime = DateTime.UtcNow,
                CreatedBy = DefaultValues.SystemId,
                UpdatedBy = DefaultValues.SystemId,
            };
            await _userRepository.AddUserAsync(user);
            center.ManagerId = user.Id;
            center.UpdatedBy = user.Id;
            await _centerRepository.UpdateCenterAsync(center);
        }

        public async Task<TokenDTO> GetJwtTokenAsync(LoginDTO login)
        {
            var user = await _userRepository.GetUsersLoginAsync(login.Username);

            //skip validate password for testing.
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }


            //if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
            //{
            //    throw new UnauthorizedAccessException("Invalid username or password");
            //}
            var accessToken = GenerateAccessToken(user);

            var refreshToken = new Token
            {
                Id = Guid.NewGuid(),
                Exp = DateTime.UtcNow.AddDays(7),
                UseNumber = 0,
                CreatedBy = user.Id,
                UpdatedBy = user.Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await _tokenRepository.AddTokenAsync(refreshToken);

            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken(refreshToken.Id, user.Id, user.LastModifiedTime)
            };
        }

        public async Task<string> GetAccessTokenAsync(string tokenId, int userId, DateTime lastModifiedTime)
        {
            var user = await _userRepository.GetUsersByIdAsync(userId);

            var token = await _tokenRepository.GetTokenByIdAsync(Guid.Parse(tokenId));
            
            if (user == null || token == null || user.LastModifiedTime != lastModifiedTime)
            {
                throw new UnauthorizedAccessException("Invalid token");
            }

            return GenerateAccessToken(user);
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName),
            };
            var expireMinutes = int.Parse(_configuration["Jwt:AccessTokenExpireMinutes"] ?? "60");

            return GenerateJwtToken(claims, DateTime.UtcNow.AddMinutes(expireMinutes));
        }
        
        private string GenerateRefreshToken(Guid RefreshTokenId, int userId, DateTime LastModifiedTime)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, RefreshTokenId.ToString()),
                new Claim(ClaimTypes.Version, LastModifiedTime.ToString("o"))
            };
            var expireDays = int.Parse(_configuration["Jwt:RefreshTokenExpireDays"] ?? "7");
            return GenerateJwtToken(claims, DateTime.UtcNow.AddDays(expireDays));
        }
        
        private string GenerateJwtToken(IEnumerable<Claim> claims,DateTime expires)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

