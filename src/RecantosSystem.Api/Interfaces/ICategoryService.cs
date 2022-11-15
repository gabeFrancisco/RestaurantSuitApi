using System.Collections.Generic;
using System.Threading.Tasks;
using RecantosSystem.Api.DTOs;

namespace RecantosSystem.Api.Interfaces
{
    public interface ICategoryService
    {
         Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    }
}