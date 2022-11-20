using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RecantosSystem.Api.Services.Logging;

namespace RecantosSystem.Api.Filters
{
	public class ExceptionsAttribute : ExceptionFilterAttribute
	{
		private readonly LogService _logService;
		public ExceptionsAttribute(LogService logService)
		{
			_logService = logService;
		}
		public override void OnException(ExceptionContext context)
		{
			var result = new ObjectResult(new
			{
				context.Exception.Message, // Or a different generic message
				context.Exception.Source,
				ExceptionType = context.Exception.GetType().FullName,
			})
			{
				StatusCode = (int)HttpStatusCode.InternalServerError
			};
            
			_logService.LogException(context.Exception);
            context.Result = result;
		}
	}
}