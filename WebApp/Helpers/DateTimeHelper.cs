using System;

namespace WebApp.Helpers
{
    public static class DateTimeHelper
    {
        public static string ToUniversalIso8601(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString("u").Replace(" ", "T");
        }
    }
}
