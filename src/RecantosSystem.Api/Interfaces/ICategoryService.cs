using System.Collections.Generic;
using System.Threading.Tasks;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Interfaces
{
    public interface ICategoryService
    {
         Task<IEnumerable<Category>> GetAllCategoriesAsync();

    }
}