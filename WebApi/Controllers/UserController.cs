using Business.Abstract;
using Business.Concrete;
using Entities.Concrete;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	public class UserController : Controller
	{
		private IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet]
		public IActionResult Register()
		{
			// Gerekirse view için bir model oluşturup döndürebilirsiniz.
			// Örneğin: return View(new RegisterViewModel());
			return View();
		}

		[HttpPost]
		public IActionResult Register(User user)
		{
			try
			{
				_userService.Register(user);
				// Başarılı bir kayıt durumu
				return RedirectToAction("RegisterOk"); // veya başka bir yönlendirme
			}
			catch (Exception ex)
			{
				// Hata durumunu ele al ve ModelState'e ekleyerek view'e geçir
				ModelState.AddModelError(string.Empty, ex.Message);
				return View(); // veya başka bir view'e yönlendir
			}
		}

		[HttpGet]
		public IActionResult RegisterOk()
		{
			// Kayıt başarılı sayfasını göster
			return View();
		}
	}
}
