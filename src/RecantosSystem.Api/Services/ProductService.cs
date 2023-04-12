using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;

namespace RecantosSystem.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public ProductService(AppDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }

        private int WorkGroupId => _userService.SelectedWorkGroup;

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products
                .Where(product => product.WorkGroupId == this.WorkGroupId)
                .Include(x => x.Category)
                .ToListAsync();

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> AddAsync(ProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new NullReferenceException("Data transfer object is null!");
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(cat => cat.Id == productDto.CategoryId);

            var product = _mapper.Map<ProductDTO, Product>(productDto);
            product.WorkGroupId = this.WorkGroupId;
            product.CreatedAt = DateTime.UtcNow;
            product.Category = category;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return productDto;
        }

        private async Task<Product> GetSingleProductAsync(int productId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(product =>
                    product.Id == productId
                    && product.WorkGroupId == this.WorkGroupId);
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var product = await this.GetSingleProductAsync(id);
            return _mapper.Map<Product, ProductDTO>(product);
        }

        public async Task<ProductDTO> UpdateAsync(ProductDTO productDto, int id)
        {
            var product = await this.GetSingleProductAsync(productDto.Id);
            var updatedProduct = _mapper.Map<ProductDTO, Product>(productDto);

            updatedProduct.UpdatedAt = DateTime.UtcNow;
            updatedProduct.CreatedAt = product.CreatedAt;
            updatedProduct.WorkGroupId = product.WorkGroupId;

            _context.Entry(product).CurrentValues.SetValues(updatedProduct);
            await _context.SaveChangesAsync();

            return _mapper.Map<Product, ProductDTO>(updatedProduct);
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}