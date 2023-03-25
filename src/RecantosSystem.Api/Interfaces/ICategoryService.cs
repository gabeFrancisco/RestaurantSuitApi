using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
	public interface ICategoryService : IBaseService<CategoryDTO>
	{
        Task<int> GetProductsCountByCategory(int categoryId);
    }
}