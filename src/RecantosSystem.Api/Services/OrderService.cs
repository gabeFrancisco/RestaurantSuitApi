using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecantosSystem.Api.Context;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private int WorkGroupId => _userService.SelectedWorkGroup;
        public OrderService(AppDbContext context, IMapper mapper, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<IEnumerable<OrderSheetDTO>> GetAllAsync()
        {
            var orderSheets = await _context.OrderSheets
                .Where(order => order.WorkGroupId == this.WorkGroupId)
                .Include(x => x.Customer)
                .Include(x => x.Table)
                .ToListAsync();

            return _mapper.Map<List<OrderSheetDTO>>(orderSheets);
        }

        public async Task<OrderSheetDTO> AddAsync(OrderSheetDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderSheetDTO> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderSheetDTO> UpdateAsync(OrderSheetDTO entity, int id)
        {
            throw new NotImplementedException();
        }
    }
}