using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Services
{
	public class CategoryService : ICategoryService, IUserAccessor
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private IUserAccessor _userAccessor;
		public CategoryService(AppDbContext context, 
                               IMapper mapper, 
                               IUserAccessor userAccessor)
		{
			_context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
		}

		public int UserId => _userAccessor.UserId;

		public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
		{
			var categories = await _context.Categories
                .Where(category => category.UserId == this.UserId)
                .ToListAsync();
                
            return _mapper.Map<List<CategoryDTO>>(categories);
		}
	}
}