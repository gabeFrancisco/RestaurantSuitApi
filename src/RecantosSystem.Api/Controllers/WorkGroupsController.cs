using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[ApiConventionType(typeof(DefaultApiConventions))]
	[Authorize(AuthenticationSchemes = "Bearer")]
	public class WorkGroupsController : ControllerBase
	{
		private readonly IWorkGroupService _workGroupService;
		public WorkGroupsController(IWorkGroupService workGroupService)
		{
			_workGroupService = workGroupService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _workGroupService.GetAllAsync());
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] WorkGroupDTO workGroupDto)
		{
			return Ok(await _workGroupService.AddAsync(workGroupDto));
 		}
	}
}