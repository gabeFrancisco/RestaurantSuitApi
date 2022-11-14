using System.Collections.Generic;
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
		public IEnumerable<Category> GetAllCategories()
		{
			return _context.Categories;
		}
	}
}