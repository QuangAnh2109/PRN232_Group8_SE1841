using Api.DTO;
using Api.Models;
using Api.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Api.Constants;

namespace Api.Services.Implement
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task RegisterAccountAsync(RegisterAccountDTO register)
        {
            var existingUser = await _context.Users
                .Where(u => u.IsDeleted == false && (u.Username == register.Username || u.Email == register.Email))
                .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                if (existingUser.Username == register.Username)
                    throw new InvalidOperationException("Username already exists");
                if (existingUser.Email == register.Email)
                    throw new InvalidOperationException("Email already exists");
            }

            var user = new User
            {
                Username = register.Username,
                FullName = register.FullName,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password),
                RoleId = DefaultValues.StudentRoleId,
                CenterId = 1,
                LastModifiedTime = DateTime.UtcNow,
                CreatedBy = DefaultValues.SystemId,
                UpdatedBy = DefaultValues.SystemId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<TokenDTO> GetJwtTokenAsync(LoginDTO login)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.IsDeleted == false && u.Username == login.Username)
                .FirstOrDefaultAsync();

            //skip validate password for testing. TODO: update logic verify Hash Password
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

            _context.Tokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = GenerateRefreshToken(refreshToken.Id)
            };
        }

        public async Task<string> GetAccessTokenAsync(int userId, DateTime lastModifiedTime)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Where(u => u.IsDeleted == false && u.Id == userId && u.LastModifiedTime == lastModifiedTime)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid user or token expired");
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
                new Claim(ClaimTypes.Version, user.LastModifiedTime.ToString("o")),
            };
            var expireMinutes = int.Parse(_configuration["Jwt:AccessTokenExpireMinutes"] ?? "60");

            return GenerateJwtToken(claims, DateTime.UtcNow.AddMinutes(expireMinutes));
        }
        
        private string GenerateRefreshToken(Guid RefreshTokenId)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, RefreshTokenId.ToString())
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

