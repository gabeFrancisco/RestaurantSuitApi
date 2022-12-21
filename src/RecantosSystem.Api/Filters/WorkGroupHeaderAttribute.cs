using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using RecantosSystem.Api.Interfaces;
using RecantosSystem.Api.Models;
using RecantosSystem.Api.Services;

namespace RecantosSystem.Api.Filters
{
    public class WorkGroupHeaderAttribute : Attribute, IAsyncResultFilter
    {
        private IUserService _userService;
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var user = await _userService.GetActualUser();

            if (user == null)
            {
                throw new UnauthorizedAccessException("Access denied!");
            }

            context.HttpContext.Response.Headers["x-workGroup-id"] = user.LastUserWorkGroup.ToString();
        }
    }
}