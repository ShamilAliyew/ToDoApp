﻿
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
        private readonly ToDoAppDbContext _DbContext;

        public UserService(ToDoAppDbContext context)
        {
            _DbContext = context;
        }

        public async Task<UserResponseDTO> AddUserAsync(UserDTO userDto)
        {
              
            bool isUserExists = await _DbContext.Users.AnyAsync(u => u.Email == userDto.Email || u.Username == userDto.Username);

            if (isUserExists)
            {
                throw new Exception("This email or username already exists!");
            }

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Username = userDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                Email = userDto.Email,
                RegisteredDate = DateTime.UtcNow
            };
            try
            {
                await _DbContext.Users.AddAsync(user);
                await _DbContext.SaveChangesAsync();
            }catch (Exception ex)
            {
                if(ex.InnerException != null)
    {
                    throw new Exception($"Inner Exception: {ex.InnerException.Message}", ex.InnerException);
                }
    else
                {
                    throw;
                }
            }
            return new UserResponseDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<UserResponseDTO> LoginAsync(string identifier, string password)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Username == identifier || u.Email == identifier);
            if(user==null || !VerifyPassword(password, user.Password))
            {
                throw new Exception("Invalid Email of Username");
            }
            return new UserResponseDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email
            }; 
        }

        public bool VerifyPassword(string enteredPassword, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword , hashPassword);

        }
    }
}
