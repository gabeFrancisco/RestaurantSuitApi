using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using RecantosSystem.Api.Interfaces;

namespace RecantosSystem.Api.Services
{
	public class HttpUserAccessor : IUserAccessor
	{
        private readonly IHttpContextAccessor _accessor;
        public HttpUserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
		public int UserId => Int32.Parse(_accessor
            .HttpContext
            .User
            .Claims
            .FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)
            .Value);
	}
}