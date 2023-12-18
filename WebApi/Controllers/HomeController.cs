using Buisness.Abstract;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Business.Abstract;
using Core.Entities;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApi.Models;


namespace WebApi.Controllers
{
	public class HomeController : Controller
	{
		private ILogger<HomeController> _logger;
		private IUserService _userService;
		private IHttpContextAccessor _httpContextAccessor;

		public HomeController(ILogger<HomeController> logger, IUserService userService, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_userService = userService;
			_httpContextAccessor = httpContextAccessor;
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

			return View();
		}

		[HttpPost]
		public IActionResult Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				
				string hashedPassword = PasswordHelper.HashPassword(model.Password);

				// Giriş işlemi
				User user = _userService.Login(model.UserName, hashedPassword);


				if (user != null)
				{

					var session = _httpContextAccessor.HttpContext?.Session;

					session.SetString("UserId", user.Id.ToString());
					session.SetString("UserName", user.UserName);



					return RedirectToAction("Ui");
				}



				ModelState.AddModelError(string.Empty, "İstifadəçi adı və ya şifrə yanlışdır");
				return View(model);
			}

			// ModelState.IsValid false ise, yani formda geçerli olmayan bir alan varsa
			// Login sayfasını tekrar göster
			return View(model);
		}

		private bool IsUserLoggedIn()
		{
			var userId = HttpContext.Session?.GetString("UserId");
			var userName = HttpContext.Session?.GetString("UserName");

			if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userName))
			{
				// İşlemlerinizi gerçekleştirin...
				// Örneğin, kullanıcı adıyla ilgili bir işlem yapmak için:
				// var user = _userService.GetByUserName(userName);

				return true;
			}

			return false;
		}
		//public IActionResult Navbar()
		//{
		//	ViewBag.IsUserLoggedIn = IsUserLoggedIn();
		//	return PartialView("_Navbar");
		//}


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
			model.SelectedRole = null;

			return View(model);
		}

		[HttpPost]
		public IActionResult Register(RegisterViewModel model)
		{

			if (ModelState.IsValid)
			{
				
				try
				{

					_userService.Register(new User()
					{
						Name = model.Name,
						SurName = model.SurName,
						UserName = model.UserName,
						Email = model.Email,
						UserDepartments = model.SelectedRole,
						

					}, PasswordHelper.HashPassword(model.Password));
					if (_userService == null)
					{
						return View(model);
					}
					return RedirectToAction("RegisterOk");

				}
				catch (Exception e)
				{
					ModelState.AddModelError(String.Empty, e.Message);
					return View(model);
				}

			}

			return View(model);



		}
		

		public IActionResult RegisterOk()
		{
			return View();

		}
		public IActionResult Logout()
		{
			// Session'daki bilgileri sil
			HttpContext.Session.Remove("UserId");
			HttpContext.Session.Remove("UserName");

			// Ana sayfaya yönlendir
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
					// Oturumu kontrol et
					var currentUserIdBytes = HttpContext.Session?.Get("UserId");
					if (currentUserIdBytes == null || currentUserIdBytes.Length == 0)
					{
						// Oturum açılmamışsa, kullanıcıyı tanımlayamayız
						return RedirectToAction("Login"); // veya başka bir işlem yapabilirsiniz
					}

					// Oturumdan kullanıcı ID'sini al
					int userId = int.Parse(Encoding.UTF8.GetString(currentUserIdBytes));

					// Kullanıcıyı veritabanından al
					var userToUpdate = _userService.GetById(userId);

					if (userToUpdate == null)
					{
						// Kullanıcı bulunamadı hatası
						ModelState.AddModelError(string.Empty, "İstifadəçi tapılmadı.");
						return View(model);
					}

					// Kullanıcı bilgilerini güncelle
					userToUpdate.Name = model.Name;
					userToUpdate.SurName = model.SurName;
					userToUpdate.UserName = model.UserName;
					userToUpdate.Email = model.Email;
					userToUpdate.PhoneNumber = model.PhoneNumber;

					// Şifre dəyişmək istəyi varsa
					if (!string.IsNullOrEmpty(model.Password))
					{
						userToUpdate.Password = model.Password;
					}

					// Kullanıcıyı güncelle
					_userService.Update(userToUpdate);

					// Başarıyla güncellendiği sayfaya yönlendir
					return RedirectToAction("Profile");
				}
				catch (Exception ex)
				{
					// Hata durumunda hata mesajını ModelState'e ekle
					ModelState.AddModelError(string.Empty, ex.Message);
				}
			}

			// ModelState.IsValid false ise, yani formda geçerli olmayan bir alan varsa
			// EditProfile sayfasını tekrar göster
			return View(model);
		}

	}
}