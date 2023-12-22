using System.IdentityModel.Tokens.Jwt;
using Core.Utilities.Security.Jwt;
using Newtonsoft.Json;

namespace WebApi.Middlewares
{
	public class ValidateTokenMiddleware
	{
		private readonly RequestDelegate _next;

		public ValidateTokenMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			//Middleware atlamalı  olduğu yolları yoxlayırıq
			var path = context.Request.Path;
			if (path.StartsWithSegments("/Auth/Login") || path.StartsWithSegments("/Auth/Register"))
			{
				// yuxarıdakı sorğular üzrə middleware'i keçirik
				await _next(context);
				return;
			}

			var token = context.Request.Cookies["AccessToken"];
			if (token != null && !IsTokenExpired(token))
			{
				// Token etibarlıdırsa, növbəti middleware'e keç
				await _next(context);
			}
			else
			{
				// Token etibarsızdırsa və ya müddəti bitibsə, istifadəçini giriş səhifəsinə yönləndiririk
				context.Response.Redirect("/Auth/Login");
			}
		}

		private bool IsTokenExpired(string token)
		{
			try
			{
				AccessToken tokenModel = JsonConvert.DeserializeObject<AccessToken>(token);
				return tokenModel.Expiration < DateTime.UtcNow;
			}
			catch(Exception ex)
			{
				return false;
			}
		}
	}
}
