using Core.Utilities.Resources.Enum;

namespace WebApi.Models
{
    public class NotificationModel
    {
        public NotificationModel()
        {
        }

        public NotificationModel(NotificationType color, string content)
        {
            HasNotification = true;
            Color = color;
            Content = content;
        }

        public bool HasNotification { get; set; }
        public NotificationType Color { get; set; }
        public string Content { get; set; }
    }
}
