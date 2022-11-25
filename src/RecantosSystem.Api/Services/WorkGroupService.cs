using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Services
{
	public class WorkGroupService : IWorkGroupService
	{
		private readonly AppDbContext _context;
		private IMapper _mapper;
		public WorkGroupService(AppDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public int UserId => throw new System.NotImplementedException();

		public int WorkGroupId => throw new System.NotImplementedException();

		public Task<WorkGroupDTO> AddAsync(WorkGroupDTO entity)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> DeleteAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public Task<IEnumerable<WorkGroupDTO>> GetAllAsync()
		{
			throw new System.NotImplementedException();
		}

		public async Task<WorkGroupDTO> GetAsync(int id)
		{

		}

		private async Task<WorkGroup> GetSingleWorkGroupAsync()
		{
            
		}

		public Task<WorkGroupDTO> UpdateAsync(WorkGroupDTO entity, int id)
		{
			throw new System.NotImplementedException();
		}
	}
}