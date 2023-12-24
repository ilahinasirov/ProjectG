using Core.Utilities.Security.Jwt;
using System.Security.Claims;
using Business.Abstract;
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
			var cookies = context.Request.Cookies;
			
			foreach (var cookie in cookies)
			{
				Console.WriteLine($"{cookie.Key}: {cookie.Value}");
			}
			var token = context.Request.Cookies["AccessToken"];
			if (!string.IsNullOrEmpty(token))
			{
				var userClaims = JwtHelper.GetClaims(token);
				var userId = userClaims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

				
				currentUserService.UserId = userId;

				
				Console.WriteLine($"JwtCurrentUserMiddleware - User ID: {userId}");
			}



			await _next(context);
		}
	}

}
