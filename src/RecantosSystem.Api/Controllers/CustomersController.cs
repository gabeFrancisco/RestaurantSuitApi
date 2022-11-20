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
			return Ok(await _customerService.GetAllAsync());
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int customerId)
		{
			return Ok(await _customerService.GetAsync(customerId));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CustomerDTO customerDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(await _customerService.AddAsync(customerDto));
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] CustomerDTO customerDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			return Ok(await _customerService.UpdateAsync(customerDTO, customerDTO.Id));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			return Ok(await _customerService.DeleteAsync(id));
		}
	}
}