using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InventoryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly Data.ApplicationDbContext _dbContext;

        public AuthService(IUserService userService, IConfiguration configuration, Data.ApplicationDbContext dbContext)
        {
            _userService = userService;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<string> GenerateJwtTokenAsync(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryInMinutes = int.Parse(jwtSettings["ExpiryInMinutes"]!);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim(ClaimTypes.Role, user.Role?.RoleName ?? ""),
                new Claim("AccountStatus", user.AccountStatus ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<User?> AuthenticateUserAsync(string email, string password)
        {
            var normalizedEmail = email?.Trim().ToLowerInvariant() ?? string.Empty;
            var isValid = await _userService.ValidateUserAsync(normalizedEmail, password);
            if (!isValid)
                return null;

            return await _userService.GetUserByEmailAsync(normalizedEmail);
        }

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            var normalizedEmail = email?.Trim().ToLowerInvariant() ?? string.Empty;
            var user = await _userService.GetUserByEmailAsync(normalizedEmail);
            return user == null;
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            var normalizedEmail = email?.Trim().ToLowerInvariant() ?? string.Empty;
            var isAvailable = await IsEmailAvailableAsync(normalizedEmail);
            if (!isAvailable)
            {
                throw new ArgumentException("Email is already in use");
            }

            // Ensure a valid role exists; prefer the 'User' role, create if missing
            var userRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
            if (userRole == null)
            {
                userRole = new Role { RoleName = "User", Description = "Regular User" };
                _dbContext.Roles.Add(userRole);
                await _dbContext.SaveChangesAsync();
            }

            var newUser = new User
            {
                Name = name,
                Email = normalizedEmail,
                Password = password,
                RoleId = userRole.RoleId,
                AccountStatus = "Active"
            };

            var created = await _userService.CreateUserAsync(newUser);
            return created;
        }
    }
}
