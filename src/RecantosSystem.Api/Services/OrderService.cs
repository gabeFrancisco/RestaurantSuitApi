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
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly ITableService _tableService;
        private int WorkGroupId => _userService.SelectedWorkGroup;
        public OrderService(AppDbContext context, 
                            IMapper mapper, 
                            IUserService userService,
                            ITableService tableService)
        {
            _context = context;
            _mapper = mapper;
            _userService = userService;
            _tableService = tableService;
        }
        public async Task<IEnumerable<OrderSheetDTO>> GetAllAsync()
        {
            var orderSheets = await _context.OrderSheets
                .Where(order => order.WorkGroupId == this.WorkGroupId)
                .Include(x => x.Customer)
                .Include(x => x.Table)
                .Include(x => x.ProductOrders)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Category)
                .ToListAsync();

            return _mapper.Map<List<OrderSheetDTO>>(orderSheets);
        }

        public async Task<OrderSheetDTO> AddAsync(OrderSheetDTO orderSheetDto)
        {
            if (orderSheetDto == null)
            {
                throw new NullReferenceException("Data transfer object is null!");
            }

            var orderSheet = _mapper.Map<OrderSheetDTO, OrderSheet>(orderSheetDto);
            orderSheet.Table = await _context.Tables
                .FirstOrDefaultAsync(table => table.Id == orderSheetDto.TableId);

            if (orderSheetDto.CustomerId != null)
            {
                orderSheet.Customer = await _context.Customers
                    .FirstOrDefaultAsync(customer => customer.Id == orderSheetDto.CustomerId);
            }

            foreach (var order in orderSheet.ProductOrders)
            {
                order.CreatedAt = DateTime.UtcNow;
                order.WorkGroupId = this.WorkGroupId;
            }

            orderSheet.WorkGroupId = this.WorkGroupId;
            orderSheet.CreatedAt = DateTime.UtcNow;
            orderSheet.OpenBy = _userService.GetActualUser().Result.Username;
           
            await _tableService.SetIsBusy(orderSheet.TableId, true, true);

            _context.OrderSheets.Add(orderSheet);
            await _context.SaveChangesAsync();

            return orderSheetDto;
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