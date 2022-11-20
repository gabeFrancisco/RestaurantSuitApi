using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Services
{
	public class TableService : ITableService
	{
		private readonly AppDbContext _context;
		private readonly IUserAccessor _userAccessor;
		private readonly IMapper _mapper;
		public TableService(AppDbContext context,
							IUserAccessor userAccessor,
							IMapper mapper)
		{
			_context = context;
			_userAccessor = userAccessor;
			_mapper = mapper;
		}

		public int UserId => _userAccessor.UserId;
		public async Task<IEnumerable<TableDTO>> GetAllAsync()
		{
			var tables = await _context.Tables
				.Where(table => table.UserId == this.UserId)
				.ToListAsync();

			return _mapper.Map<List<TableDTO>>(tables);
		}

		private async Task<Table> GetSingleTableAsync(int tableId)
		{
            return await _context.Tables 
                .FirstOrDefaultAsync(
                    table => table.Id == tableId
                    && table.UserId == this.UserId
                );
		}

		public Task<TableDTO> GetAsync(int id)
		{
			throw new System.NotImplementedException();
		}

		public Task<TableDTO> AddAsync(TableDTO entity)
		{
			throw new System.NotImplementedException();
		}

		public Task<TableDTO> UpdateAsync(TableDTO entity, int id)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> DeleteAsync(int id)
		{
			throw new System.NotImplementedException();
		}
	}
}