using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Business.Abstract;
using Core.Entities;
using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Json;
using WebApi.Models;
using JsonConvert = Newtonsoft.Json.JsonConvert;


namespace WebApi.Controllers
{
	public class HomeController : Controller
	{
		private ILogger<HomeController> _logger;
		private IUserService _userService;
		private IHttpContextAccessor _httpContextAccessor;
		private readonly ICurrentUserService _currentUserService;

		public HomeController(ILogger<HomeController> logger, IUserService userService, IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
		{
			_logger = logger;
			_userService = userService;
			_httpContextAccessor = httpContextAccessor;
			_currentUserService = currentUserService;
		}






		public IActionResult Index()
		{
			
			
			return View();
		}

		public IActionResult Ui()
		{
			string cookieData = Request.Cookies["AccessToken"];
			if (!string.IsNullOrEmpty(cookieData))
			{
				AccessToken loggedInUser = JsonConvert.DeserializeObject<AccessToken>(cookieData);
			}
			
			return View();
		}
		public IActionResult TestUi()

		{
			

			return View("TestUi");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Login()
		{

			return View();
		}

	


		public IActionResult RegisterOk()
		{
			return View();

		}
		public IActionResult Logout()
		{
			// user məlumatlarını silmliyik
			//HttpContext.Session.Remove("UserId");
			//HttpContext.Session.Remove("UserName");

			// yönləndirmə
			return RedirectToAction("TestUi", "Home");
		}

		public IActionResult Profile()
		{
			return View();
		}

		public IActionResult EditProfile()
		{
			return View();
		}

		[HttpPost]
		public IActionResult EditProfile(User model)
		{
			if (ModelState.IsValid)
			{
				try
				{

					return RedirectToAction("Profile");
				}
				catch (Exception ex)
				{

					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}


			return View(model);
		}

	}
}