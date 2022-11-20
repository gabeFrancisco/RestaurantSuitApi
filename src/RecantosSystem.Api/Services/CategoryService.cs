using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;
using RecantosSystem.Api.Services.Logging;

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

		/// <summary>
		/// Returns all the categories from the database
		/// </summary>
		/// <returns>A List object containing CategoryDTOs</returns>
		public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
		{
			var categories = await _context.Categories
				.Where(cat => cat.UserId == this.UserId)
				.ToListAsync();

			return _mapper.Map<List<CategoryDTO>>(categories);
		}

		/// <summary>
		/// Create a new category to insert into database
		/// </summary>
		/// <param name="categoryDTO">Category data transfer object</param>
		/// <returns></returns>
		public async Task<CategoryDTO> AddAsync(CategoryDTO categoryDTO)
		{
			if (categoryDTO == null)
			{
				throw new NullReferenceException("Data transfer object is null!");
			}

			var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
			category.UserId = this.UserId;
			category.CreatedAt = DateTime.UtcNow;

			_context.Categories.Add(category);
			await _context.SaveChangesAsync();

			return categoryDTO;
		}

		private async Task<Category> GetSingleCategoryAsync(int categoryId)
		{
			return await _context.Categories
				.FirstOrDefaultAsync(cat =>
					cat.Id == categoryId
					&& cat.UserId == this.UserId
			);
		}

		public async Task<CategoryDTO> GetAsync(int categoryId)
		{
			var category = await this.GetSingleCategoryAsync(categoryId);
			return _mapper.Map<Category, CategoryDTO>(category);
		}
		public async Task<CategoryDTO> UpdateAsync(CategoryDTO categoryDto, int categoryId)
		{
			var category = await this.GetSingleCategoryAsync(categoryId);
			var updatedCategory = _mapper.Map<CategoryDTO, Category>(categoryDto);

			updatedCategory.UpdatedAt = DateTime.UtcNow;
			updatedCategory.CreatedAt = category.CreatedAt;
			updatedCategory.UserId = category.UserId;

			_context.Entry(category).CurrentValues.SetValues(updatedCategory);
			await _context.SaveChangesAsync();

			return _mapper.Map<Category, CategoryDTO>(updatedCategory);
		}

		public async Task<bool> DeleteAsync(int categoryId)
		{
			var category = await this.GetSingleCategoryAsync(categoryId);

			_context.Categories.Remove(category);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}