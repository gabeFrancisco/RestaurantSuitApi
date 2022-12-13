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
		private readonly IUserService _userService;
		public CustomerService(AppDbContext context, IMapper mapper, IUserService userService)
		{
			_context = context;
			_mapper = mapper;
			_userService = userService;
		}
		private int WorkGroupId => _userService.SelectedWorkGroup;
		private async Task<Customer> GetSingleCustomerAsync(int customerId)
		{
			return await _context.Customers
				.FirstOrDefaultAsync(
					customer => customer.Id == customerId
					&& customer.WorkGroupId == this.WorkGroupId
				);
		}

		public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
		{
			var customers = await _context.Customers
				.Where(customer => customer.WorkGroupId == this.WorkGroupId)
				.ToListAsync();

			return _mapper.Map<List<CustomerDTO>>(customers);
		}

		public async Task<CustomerDTO> GetAsync(int customerId)
		{
			var customer = await this.GetSingleCustomerAsync(customerId);
			return _mapper.Map<Customer, CustomerDTO>(customer);
		}

		public async Task<CustomerDTO> AddAsync(CustomerDTO customerDto)
		{
			if (customerDto == null)
			{
				throw new NullReferenceException("Data transfer object is null");
			}

			var customer = _mapper.Map<CustomerDTO, Customer>(customerDto);
			customer.WorkGroupId = this.WorkGroupId;
			customer.CreatedAt = DateTime.UtcNow;

			_context.Customers.Add(customer);
			await _context.SaveChangesAsync();

			return customerDto;
		}
		public async Task<CustomerDTO> UpdateAsync(CustomerDTO customerDto, int customerId)
		{
			var customer = await this.GetSingleCustomerAsync(customerId);
			var updateCustomer = _mapper.Map<CustomerDTO, Customer>(customerDto);

			updateCustomer.UpdatedAt = DateTime.UtcNow;
			updateCustomer.CreatedAt = customer.CreatedAt;
			updateCustomer.WorkGroupId = customer.WorkGroupId;

			_context.Entry(customer).CurrentValues.SetValues(updateCustomer);
			await _context.SaveChangesAsync();

			return _mapper.Map<Customer, CustomerDTO>(customer);
		}
		public async Task<bool> DeleteAsync(int customerId)
		{
			var customer = await this.GetSingleCustomerAsync(customerId);
			_context.Customers.Remove(customer);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}