using System.Collections.Generic;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Interfaces
{
    public interface ICategoryService
    {
         IEnumerable<Category> GetAllCategories();
    }
}