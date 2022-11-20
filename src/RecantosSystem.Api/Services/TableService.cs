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
            table.UserId = this.UserId;
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
            updatedTable.UserId = this.UserId;

            _context.Entry(table).CurrentValues.SetValues(updatedTable);
            await _context.SaveChangesAsync();

            return  _mapper.Map<Table, TableDTO>(updatedTable);
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