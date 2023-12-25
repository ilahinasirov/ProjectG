using Core.Utilities.Resources.Enum;

namespace Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToAlertColor(this NotificationType src)
        {
            return $"alert-{src.ToString().ToLower()}";
        }

        public static string ToCustomFormat(this DateTime src)
        {
            return src.ToString("dd.MM.yyyy HH:mm");
        }
    }
}
