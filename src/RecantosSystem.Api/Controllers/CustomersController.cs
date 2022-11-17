using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
	}
}