using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;
using RecantosSystem.Api.Models.Enums;

namespace RecantosSystem.Api.Services
{
	public class WorkGroupService : IWorkGroupService
	{
		private readonly AppDbContext _context;
		private IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IHttpContextAccessor _accessor;
		public WorkGroupService(AppDbContext context,
								IMapper mapper,
								IUserService userService,
								IHttpContextAccessor accessor)
		{
			_context = context;
			_mapper = mapper;
			_userService = userService;
			_accessor = accessor;
		}

		public User User => _userService.GetUser();

		public async Task<WorkGroupDTO> AddAsync(WorkGroupDTO entity)
		{
			//Checks if the entity is null
			if (entity == null)
			{
				throw new NullReferenceException("Data transfer object cannot be null!");
			}

			var workGroup = _mapper.Map<WorkGroupDTO, WorkGroup>(entity);

			//Checks if the actual User is an administrator
			//If not, it will throw an excepion
			if (this.User.Role == Role.WORKER)
			{
				throw new UnauthorizedAccessException(
					"You cannot add this WorkGroup because the user is of the Role.WORKER type!"
					);
			}

			workGroup.Administrator = this.User;
			var workersIds = entity.Workers;

			if (!workersIds.Any())
			{
				//Go through each given and creates a new UserWorkGroup object
				//to be added to the database
				foreach (var id in workersIds)
				{
					var user = await _userService.ReadUser(id);
					if (user == null)
					{
						break;
					}

					UserWorkGroup userWorkGroup = new UserWorkGroup()
					{
						CreatedAt = DateTime.UtcNow,
						User = user,
						WorkGroup = workGroup
					};

					_context.UserWorkGroups.Add(userWorkGroup);
				}
			}

			_context.WorkGroups.Add(workGroup);
			await _context.SaveChangesAsync();

			return entity;
		}

		public Task<bool> DeleteAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public async Task<IEnumerable<WorkGroupDTO>> GetAllAsync()
		{
			var workGroups = await _context.UserWorkGroups
				.Where(uwg => uwg.UserId == this.User.Id)
				.Include(uwg => uwg.WorkGroup)
				.Select(uwg => uwg.WorkGroup)
				.ToListAsync();
			
			return _mapper.Map<List<WorkGroupDTO>>(workGroups);
		}

		public async Task<WorkGroupDTO> GetAsync(int id)
		{
			var workGroup = await this.GetSingleWorkGroupAsync(id);
			return _mapper.Map<WorkGroup, WorkGroupDTO>(workGroup);
		}

		private async Task<WorkGroup> GetSingleWorkGroupAsync(int id)
		{
			return await _context.WorkGroups
				.Where(wg => wg.Id == id)
				.FirstOrDefaultAsync();
		}

		public Task<WorkGroupDTO> UpdateAsync(WorkGroupDTO entity, int id)
		{
			throw new System.NotImplementedException();
		}

		public async Task<WorkGroupDTO> SelectWorkGroup(int id)
		{
			var workGroup = await this.GetSingleWorkGroupAsync(id);
			if (workGroup == null)
			{
				throw new NullReferenceException("The Work Group does not exist!");
			}

			_accessor.HttpContext.Request.Headers.Add("X-WorkGroupId", id.ToString());
			return _mapper.Map<WorkGroup, WorkGroupDTO>(workGroup);
		}
	}
}