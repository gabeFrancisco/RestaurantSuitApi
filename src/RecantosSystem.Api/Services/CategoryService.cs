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
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private IUserService _userService;
        public CategoryService(AppDbContext context,
                               IMapper mapper,
                               IUserService userService)
        { 
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        private int WorkGroupId => _userService.SelectedWorkGroup;

        /// <summary>
        /// Returns all the categories from the database
        /// </summary>
        /// <returns>A List object containing CategoryDTOs</returns>
        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories
                .Where(cat => cat.WorkGroupId == this.WorkGroupId)
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
            var actualUser = await _userService.GetActualUser();
            if (categoryDTO == null)
            {
                throw new NullReferenceException("Data transfer object is null!");
            }

            var category = _mapper.Map<CategoryDTO, Category>(categoryDTO);
            category.WorkGroupId = this.WorkGroupId;
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
                    && cat.WorkGroupId == this.WorkGroupId
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
            updatedCategory.WorkGroupId = category.WorkGroupId;

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