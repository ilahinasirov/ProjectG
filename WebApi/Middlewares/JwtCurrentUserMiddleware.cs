using Core.Utilities.Security.Jwt;
using System.Security.Claims;
using Business.Abstract;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace WebApi.Middlewares
{
	public class JwtCurrentUserMiddleware
	{
		private readonly RequestDelegate _next;
	
		public JwtCurrentUserMiddleware(RequestDelegate next)
		{
			_next = next;
			
		}

		public async Task Invoke(HttpContext context, ICurrentUserService currentUserService)
		{

			//string token = context.RequestServices.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Cookies["AccessToken"];

			var token = context.Request.Cookies["AccessToken"];

			if (!string.IsNullOrEmpty(token))
			{
				var userClaims = JwtHelper.GetClaims(token);
				var userId = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
				currentUserService.UserId = userId;
			}





			await _next(context);
		}
	}

}
