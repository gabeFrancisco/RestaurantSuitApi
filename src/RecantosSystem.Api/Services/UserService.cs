using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;
using RecantosSystem.Api.Models.Enums;
using RecantosSystem.Api.Services.Logging;
using RecantosSystem.Api.Services.Security;

namespace RecantosSystem.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public UserService(AppDbContext context,
                           TokenService tokenService,
                           IMapper mapper,
                           IHttpContextAccessor accessor)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
            _accessor = accessor;
        }

        public int SelectedWorkGroup => Int32.Parse(_accessor
            .HttpContext
            .Request
            .Headers
            .FirstOrDefault(x => x.Key == "x-workGroup-id").Value);

        public int UserId => Int32.Parse(_accessor
                    .HttpContext
                    .User
                    .Claims
                    .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)
                    .Value);

        public async Task<User> GetActualUser()
        {
            try
            {
                return await this.GetSingleUserAsync(this.UserId);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public async Task<User> GetSingleUserAsync(int id)
        {
            return await _context.Users
                .Where(user => user.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<dynamic> RegisterUser(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new NullReferenceException("User data cannot be null");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = _mapper.Map<UserDTO, User>(userDto);
            user.Password = passwordHash;
            user.CreatedAt = DateTime.UtcNow;
            user.Role = Role.WORKER;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Password = "**********";

            return new
            {
                Message = $"User {userDto.Username} registered with success!"
            };
        }

        public async Task<dynamic> Login(LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                throw new NullReferenceException("Login data is null!");
            }

            var user = await _context.Users
                .Where(x => x.Username == loginDto.Username)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return "Username is incorrect";
            }

            bool verified = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password);
            if (!verified)
            {
                return "Password is incorrect";
            }

            _accessor.HttpContext.Response.Headers["x-workGroup-id"] = user.LastUserWorkGroup.ToString();

            return new
            {
                User = user,
                Token = _tokenService.GenerateJwTToken(user.Username, user.Id, user.Role.ToString()),
                Message = $"The user {user.Username} was logged succesfully!"
            };
        }

        public Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> AddAsync(UserDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> UpdateAsync(UserDTO userDto, int userId)
        {
            var user = await this.GetSingleUserAsync(userId);
            var updatedUser = _mapper.Map<UserDTO, User>(userDto);

            updatedUser.UpdatedAt = DateTime.UtcNow;
            updatedUser.CreatedAt = user.CreatedAt;

            _context.Entry(user).CurrentValues.SetValues(updatedUser);
            await _context.SaveChangesAsync();

            return _mapper.Map<User, UserDTO>(updatedUser);
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateUserLastWorkGroupId(int workGroupId)
        {
            var user = await this.GetActualUser();
            user.LastUserWorkGroup = workGroupId;

            _context.Entry(user).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}