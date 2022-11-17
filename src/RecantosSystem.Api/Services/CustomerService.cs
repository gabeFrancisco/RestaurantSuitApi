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
	public class CustomerService : ICustomerService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		private readonly IUserAccessor _userAccessor;
		public CustomerService(AppDbContext context, IMapper mapper, IUserAccessor userAccessor)
		{
			_context = context;
			_mapper = mapper;
			_userAccessor = userAccessor;
		}
		public int UserId => _userAccessor.UserId;

		public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
		{
			var customers = await _context.Customers
				.Where(customer => customer.UserId == this.UserId)
				.ToListAsync();

			return _mapper.Map<List<CustomerDTO>>(customers);
		}
		public async Task<CustomerDTO> AddAsync(CustomerDTO customerDto)
		{
			if (customerDto == null)
			{
				throw new NullReferenceException("Data transfer object is null");
			}

			var customer = _mapper.Map<CustomerDTO, Customer>(customerDto);
			customer.UserId = this.UserId;
			customer.CreatedAt = DateTime.UtcNow;

			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			return customerDto;
		}

		public Task<bool> DeleteAsync(int customerId)
		{
			throw new System.NotImplementedException();
		}

		public Task<CustomerDTO> GetAsync(int customerId)
		{
			throw new System.NotImplementedException();
		}

		public Task<CustomerDTO> UpdateAsync(CustomerDTO customerDto)
		{
			throw new System.NotImplementedException();
		}
	}
}