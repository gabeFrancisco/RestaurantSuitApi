using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecantosSystem.Api.DTOs;
using RecantosSystem.Api.Filters;
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

        [HttpGet("getWorkGroupId")]
        public IActionResult GetWg()
        {
            return Ok("Header returned with success!");
        }

        [HttpPost("selectWorkGroup/{id}")]
        public async Task<IActionResult> SelectWorkGroup(int id)
        {
            return Ok(await _workGroupService.SelectWorkGroup(id));
        }
    }
}