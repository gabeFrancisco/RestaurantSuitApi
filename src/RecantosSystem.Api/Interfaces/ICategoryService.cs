using System.Collections.Generic;
using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
	public interface ICategoryService
	{
        int UserId { get; }
		Task<IEnumerable<CategoryDTO>> GetAllAsync();
		Task<CategoryDTO> GetAsync(int categoryId);
		Task<CategoryDTO> AddAsync(CategoryDTO categoryDTO);
		Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDto, int categoryId);
        Task<bool> DeleteAsync(int categoryId);
	}
}