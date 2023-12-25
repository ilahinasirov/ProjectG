using Business.Abstract;
using Core.Extensions;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApi.PresentationValidator.FluentValidation;

namespace WebApi.Controllers
{
	public class AuthController : BaseController
	{
		private IAuthService _authService;
		private IUserService _userService;


		public AuthController(IAuthService authService, IUserService userService)
		{
			_authService = authService;
			_userService = userService;
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
			var staticDepartmens = new StaticDepartmens();
			var departments = _userService.GetAllDepartments();

			var model = new UserForRegisterDto
			{
				Departments = departments.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList()
			};
			return View(model);
		}

		[HttpPost]
		public IActionResult Register(UserForRegisterDto model)
		{
			var validator = new RegisterValidatior();
			var validationResult = validator.Validate(model);
			model.Departments = model.Departments ?? _userService.GetAllDepartments().Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

			if (!validationResult.IsValid)
			{
				validationResult.Errors.ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
				return View(model);
			}

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
	}
}
