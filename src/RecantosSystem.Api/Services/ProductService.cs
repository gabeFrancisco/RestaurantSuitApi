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
                .ToListAsync();

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public async Task<ProductDTO> AddAsync(ProductDTO productDto)
        {
            if (productDto == null)
            {
                throw new NullReferenceException("Data transfer object is null!");
            }

            var product = _mapper.Map<ProductDTO, Product>(productDto);
            product.WorkGroupId = this.WorkGroupId;
            product.CreatedAt = DateTime.UtcNow;

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

        public Task<ProductDTO> UpdateAsync(ProductDTO entity, int id)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}