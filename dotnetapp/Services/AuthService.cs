using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;

namespace dotnetapp.Services
{
    public class AuthService : IAuthService
    {
        private ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext db, IConfiguration configuration)
        {
            _context = db;
            _configuration = configuration;
        }


        public async Task<(int, string)> Register(User model, string role)
        {
            try
            {
                if (!IsValidRole(role))
                {
                    return (0, "Invalid role");
                }
                var isExist = _context.Users.FirstOrDefault(ValueTask=>ValueTask.Email == model.Email);
                if(isExist !=null)
                {
                    return (2, "User Already Exist!");
                }
                model.Password = HashPassword(model.Password);
                await _context.Users.AddAsync(model);
                await _context.SaveChangesAsync();
                return (1, "Registration Successful");
            }
            catch (Exception e)
            {

                return (0, "Error Occurs");
            }
        }

        public async Task<(int, string)> Login(LoginModel model)
        {
            var _user = _context.Users.FirstOrDefault(e => e.Email == model.Email);
            if (_user == null)
            {
                return (0, "Invalid email");
            }
            if (!VerifyPassword(model.Password, _user.Password))
            {
                return (0, "Invalid password");
            }
            var claims = new List<Claim>
                {
                    // property of User
                    new Claim(ClaimTypes.NameIdentifier, _user.UserId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, _user.Username.ToString()),
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.Role, _user.UserRole)
                };

            var token = GenerateToken(claims);
            return (1, token);
        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public bool IsValidRole(string role)
        {
            List<string> validRoles = new List<string> { "Admin", "User" };

            if (!validRoles.Contains(role, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
    }
}
