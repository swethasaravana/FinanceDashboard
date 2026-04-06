using FinanceDashboardAPI.Context;
using FinanceDashboardAPI.Interfaces;
using FinanceDashboardAPI.Models.DTOs;
using FinanceDashboardAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceDashboardAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;


        public AuthService(IUserRepository userRepo, AppDbContext context, IConfiguration config)
        {
            _userRepo = userRepo;
            _context = context;
            _config = config;
        }

        public async Task<string> Register(RegisterDTO dto)
        {
            // 1. Check email exists
            var existingUser = await _userRepo.GetByEmail(dto.Email);
            if (existingUser != null)
                return "Email already exists";

            // 2. Get default role (Viewer)
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == "Admin");

            if (role == null)
                return "Default role not found";

            // 3. Hash password
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = role.RoleId
            };

            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            // 4. Save
            await _userRepo.AddUser(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        public async Task<string> Login(LoginDTO dto)
        {
            var user = await _userRepo.GetByEmail(dto.Email);

            if (user == null)
                return "Invalid email";

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return "Invalid password";

            // Create Claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            };

            // 🔐 Create Token
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> CreateUserByAdmin(CreateUserByAdminDTO dto)
        {
            // 1. Check existing user
            var existingUser = await _userRepo.GetByEmail(dto.Email);
            if (existingUser != null)
                return "Email already exists";

            // 2. Get role from DB
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == dto.RoleName);

            if (role == null)
                return "Invalid role";

            // 3. Hash password
            var hasher = new PasswordHasher<User>();
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                RoleId = role.RoleId
            };

            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            // 4. Save
            await _userRepo.AddUser(user);
            await _context.SaveChangesAsync();

            return "User created successfully";
        }

        public async Task<List<UserResponseDTO>> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsers();

            return users.Select(u => new UserResponseDTO
            {
                Id = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.RoleName,
                IsActive = u.IsActive
            }).ToList();
        }

        public async Task<string> ToggleUserStatus(int userId)
        {
            var user = await _userRepo.GetById(userId);

            if (user == null)
                return "User not found";

            user.IsActive = !user.IsActive;

            await _context.SaveChangesAsync();

            return user.IsActive ? "User Activated" : "User Deactivated";
        }

        public async Task<string> UpdateUserRole(int userId, UpdateUserRoleDTO dto)
        {
            var user = await _userRepo.GetById(userId);
            if (user == null)
                return "User not found";

            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleName == dto.RoleName);

            if (role == null)
                return "Invalid role";

            user.RoleId = role.RoleId;

            await _context.SaveChangesAsync();

            return "User role updated successfully";
        }
    }
}
