using Buisness.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApi.Models;


namespace WebApi.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IUserService _userService;

		public HomeController(ILogger<HomeController> logger, IUserService userService)
		{
			_logger = logger;
			_userService = userService;
		}


		



		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Ui()
		{
			return View();
		}
		public IActionResult TestUi()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		public IActionResult Login()
		{
			//girish kontrolu
			//sessionda user saxlanmasi
			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			return View();
		}

		public IActionResult Register()
		{
			// username kontrolu varmi yoxmu
			// eposta kontrolu
			// signup ishlemleri
			var model = new RegisterViewModel();

			// Diğer gerekli işlemler...

			// Örnek: Rolleri bir veritabanından alıyorsanız:
			// model.AvailableRoles = _roleService.GetAllRoles();

			// Örnek: Elle tanımlı bir rol listesi ekliyorsanız:
			model.AvailableRoles = new List<string> { "Admin", "User", "Guest" };

			// Başlangıçta seçili rolü belirtmek için:
			model.SelectedRole =null;

			return View(model);
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{

			if (ModelState.IsValid)
			{
				User user = null;
				try
				{  
					
					_userService.Register(new User()
					{
						Name = model.Name,
						SurName = model.SurName,
						UserName = model.UserName,
						Email = model.Email,
						UserDepartments = model.SelectedRole,
						Password = model.Password
						
					});
					if (_userService==null )
					{
						return View(model);
					}
					return RedirectToAction("RegisterOk");

				}
				catch (Exception e)
				{
					ModelState.AddModelError(String.Empty,e.Message );
					throw;
				}

			}

			return View(model);



		}

		public IActionResult RegisterOk()
		{
			return View();

		}
	}
}