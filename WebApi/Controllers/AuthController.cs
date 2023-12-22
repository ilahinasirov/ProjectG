using System.Text;
using Business.Abstract;
using Business.Constants;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
	public class AuthController : Controller
	{
		private IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Login(UserForLoginDto model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var userToLogin = _authService.Login(model);
					if (!userToLogin.Success)
					{
						return BadRequest(userToLogin.Message);
					}

					var result = _authService.CreateAccessToken(userToLogin.Data);
					if (result.Success)
					{
						string jwtObjectString = Newtonsoft.Json.JsonConvert.SerializeObject(result.Data);
						Response.Cookies.Append("AccessToken", jwtObjectString);
						return RedirectToAction("Ui", "Home");
					}

					return BadRequest(result.Message);
				}
				catch (Exception e)
				{
					ModelState.AddModelError(String.Empty, e.Message);
					return View(model);
				}

			
			}

			return View(model);
		}


		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Register(UserForRegisterDto model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var userAlreadyExists = _authService.UserAlreadyExists(model.UserName);
					if (!userAlreadyExists.Success)
					{
						return BadRequest(userAlreadyExists.Message);
					}

					var registerResult = _authService.Register(model, model.Password);

					var result = _authService.CreateAccessToken(registerResult.Data);
					if (result.Success)
					{
						return RedirectToAction("RegisterOk", "Home");
					}
					return View("Error");
				}
				catch (Exception e)
				{
					ModelState.AddModelError(String.Empty, e.Message);
					return View(model);
				}


			}

			return View(model);
		}
	}
}
