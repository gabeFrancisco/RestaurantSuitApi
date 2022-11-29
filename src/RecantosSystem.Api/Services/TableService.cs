using System;
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
		private readonly IUserService _userService;
		private readonly IMapper _mapper;
		public TableService(AppDbContext context,
							IUserService userService,
							IMapper mapper)
		{
			_context = context;
			_userService = userService;
			_mapper = mapper;
		}

		public User User => _userService.GetUser();
		private int WorkGroupId => _userService.SelectedWorkGroup;

		public async Task<IEnumerable<TableDTO>> GetAllAsync()
		{
			var tables = await _context.Tables
				.Where(table => table.WorkGroupId == this.WorkGroupId)
				.ToListAsync();

			return _mapper.Map<List<TableDTO>>(tables);
		}

		private async Task<Table> GetSingleTableAsync(int tableId)
		{
			return await _context.Tables
				.FirstOrDefaultAsync(
					table => table.Id == tableId
					&& table.WorkGroupId == this.WorkGroupId
				);
		}

		public async Task<TableDTO> GetAsync(int id)
		{
			var table = await this.GetSingleTableAsync(id);
			return _mapper.Map<Table, TableDTO>(table);
		}

		public async Task<TableDTO> AddAsync(TableDTO tableDto)
		{
			if (tableDto == null)
			{
				throw new NullReferenceException("Table data transfer is null");
			}

			var table = _mapper.Map<TableDTO, Table>(tableDto);
			table.WorkGroupId = this.WorkGroupId;
			table.CreatedAt = DateTime.UtcNow;

			_context.Tables.Add(table);
			await _context.SaveChangesAsync();

			return tableDto;
		}

		public async Task<TableDTO> UpdateAsync(TableDTO tableDto, int id)
		{
			var table = await this.GetSingleTableAsync(id);
			var updatedTable = _mapper.Map<TableDTO, Table>(tableDto);

			updatedTable.UpdatedAt = DateTime.UtcNow;
			updatedTable.CreatedAt = table.CreatedAt;
			updatedTable.WorkGroupId = this.WorkGroupId;

			_context.Entry(table).CurrentValues.SetValues(updatedTable);
			await _context.SaveChangesAsync();

			return _mapper.Map<Table, TableDTO>(updatedTable);
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var table = await this.GetSingleTableAsync(id);

			_context.Tables.Remove(table);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}