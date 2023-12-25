using Business.Abstract;
using Core.Entities.Concrete;
using Core.Services;
using Core.Utilities.Resources.Enum;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Models;
using WebApi.Models.ViewModels;
using JsonConvert = Newtonsoft.Json.JsonConvert;


namespace WebApi.Controllers
{
    public class HomeController : BaseController
	{
		private ILogger<HomeController> _logger;
		private IUserService _userService;
		private IHttpContextAccessor _httpContextAccessor;
		private readonly ICurrentUserService _currentUserService;

		public HomeController(ILogger<HomeController> logger, IUserService userService, IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
			_currentUserService = currentUserService;
			_userService = userService;
		}






		public IActionResult Index()
		{
			
			
			return View();
		}

		public IActionResult Ui()
		{
			AccessToken cookieData = JsonConvert.DeserializeObject<AccessToken>(Request.Cookies["AccessToken"]);

			var notificationModel = GetNotification();

            var userId = GetUserId();

			var userRequests = _userService.GetUserRequests(userId);

			var result = new HomeUiViewModel
			{
                Requests = userRequests,
				Notification = notificationModel
            };

			return View(result);
		}

		[HttpPost]
		public IActionResult UpdateRequest(string actionName, string comment, int requestId)
		{
			var loggedInUser = _userService.GetById(GetUserId());
			var actionType = (ActionType)Enum.Parse(typeof(ActionType), actionName);

            var userRequest = new UserRequest(loggedInUser.Id, requestId, comment, loggedInUser.UserName, actionType == ActionType.ConfirmRequest);

            var updateResult = _userService.UpdateUserRequests(userRequest, actionType);

			var user = _userService.GetById(GetUserId());

			if (updateResult > 0)
			{
				SetNotification($"{requestId} nömrəli Tələb {user.UserName} tərəfindən redaktə olundu.", NotificationType.Success);
			}
			else
			{
                SetNotification("Xəta baş verdi. Məlumat redaktə olunmadı.", NotificationType.Fail);
            }

            return RedirectToAction("Ui", "Home");
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