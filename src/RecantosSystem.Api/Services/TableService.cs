using System.Collections.Generic;
using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Services
{
	public class TableService : ITableService
	{
		public int UserId => throw new System.NotImplementedException();
		public Task<IEnumerable<TableDTO>> GetAllAsync()
		{
			throw new System.NotImplementedException();
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