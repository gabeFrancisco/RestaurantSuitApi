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
    public class WorkGroupHeaderAttribute : Attribute, IAsyncResourceFilter
    {
        private IUserService _userService;
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
             _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var user = await _userService.GetActualUser() ?? null;

            if (user == null)
            {
                await next();
            }
            else
            {
                context.HttpContext.Response.Headers["x-workGroup-id"] = user.LastUserWorkGroup.ToString();
                await next();
            }
        }
    }
}