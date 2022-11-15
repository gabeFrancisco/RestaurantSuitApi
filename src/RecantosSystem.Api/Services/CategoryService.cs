using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Services
{
	public class CategoryService : ICategoryService
	{
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
		public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
		{
			return await _context.Categories.ToListAsync();
		}
	}
}