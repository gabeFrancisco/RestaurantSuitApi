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
    public class WorkGroupHeaderAttribute : Attribute, IAsyncActionFilter
    {
        private IUserService _userService;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _userService = (IUserService)context.HttpContext.RequestServices.GetService(typeof(IUserService));
            var user = await _userService.GetActualUser() ?? null;

            if (user == null)
            {
                await next();
            }

            context.HttpContext.Response.Headers["x-workGroup-id"] = user.LastUserWorkGroup.ToString();
        }

    }
}