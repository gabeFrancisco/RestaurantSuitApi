using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
		private LogService _logService;

		public UserService(AppDbContext context,
						   TokenService tokenService,
						   IMapper mapper,
						   LogService logService) 
		{
			_context = context;
			_tokenService = tokenService;
			_mapper = mapper;
			_logService = logService;
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
			_logService.LogMessage($"User {userDto.Username} registered with success!");

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
			_logService.LogMessage($"User {loginDto.Username} signed in with success!");

			return new
			{
				User = user,
				Token = _tokenService.GenerateJwTToken(user.Username, user.Id),
				Message = $"The user {user.Username} was logged succesfully!"
			};
		}
	}
}