using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiConventionType(typeof(DefaultApiConventions))]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class CustomersController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				return Ok(await _customerService.GetAllAsync());
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while fetching all customers."
				);
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int customerId)
		{
			try
			{
				return Ok(await _customerService.GetAsync(customerId));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while reading customers."
				);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CustomerDTO customerDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				return Ok(await _customerService.AddAsync(customerDto));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while posting customers."
				);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CustomerDTO customerDTO)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				return Ok(await _customerService.UpdateAsync(customerDTO, customerDTO.Id));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while updating the customer"
				);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				return Ok(await _customerService.DeleteAsync(id));
			}
			catch
			{
				return StatusCode(
					StatusCodes.Status500InternalServerError,
					"An error has occurred while removing the customer"
				);
			}
		}
	}
}