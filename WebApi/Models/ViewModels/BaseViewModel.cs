using Core.Utilities.Resources.Enum;

namespace WebApi.Models.ViewModels
{
    public class BaseViewModel
    {
        private NotificationModel _notificationModel;

        public NotificationModel Notification
        {
            get
            {
                _notificationModel = _notificationModel ?? new NotificationModel
                {
                    HasNotification = false,
                    Color = NotificationType.Success
                };

                return _notificationModel;
            }
            set
            {
                if (value is null)
                {
                    _notificationModel = new NotificationModel
                    {
                        HasNotification = false,
                        Color = NotificationType.Success
                    };
                }
                else
                {
                    _notificationModel = value;
                }
            }
        }
    }
}
