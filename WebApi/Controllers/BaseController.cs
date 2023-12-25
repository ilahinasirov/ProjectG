using Core.Utilities.Resources.Enum;
using Core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        protected int GetUserId()
        {
            AccessToken cookieData = JsonConvert.DeserializeObject<AccessToken>(Request.Cookies["AccessToken"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(cookieData.Token) as JwtSecurityToken;

            if (jwtToken == null)
                return -1;

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return -1;

            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            return -1;
        }


        protected void SetNotification(string messageContent, NotificationType color, string key = "notification")
        {
            var notificationType = color;
            var notification = new NotificationModel(notificationType, messageContent);
            TempData[key] = JsonConvert.SerializeObject(notification);
        }

        protected NotificationModel GetNotification(string key = "notification")
        {
            var tempData = TempData[key];

            if (tempData is null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<NotificationModel>(tempData.ToString());
        }
    }
}
