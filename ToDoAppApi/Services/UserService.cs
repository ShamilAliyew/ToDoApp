
using System.Diagnostics.Metrics;
using System;
using ToDoAppApi.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using ToDoAppApi.DTOs;

namespace ToDoAppApi.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoAppDbContext _context;

        public UserService(ToDoAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserResponseDTO> AddUserAsync(UserDTO userDto)
        {
            // Email və Username mövcuddurmu?  
            bool isUserExists = await _context.Users.AnyAsync(u => u.Email == userDto.Email || u.Username == userDto.Username);

            if (isUserExists)
            {
                throw new Exception("Bu email və ya username artıq mövcuddur!");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password), 
                Email = userDto.Email
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new UserResponseDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email
            };
        }

    }
}
