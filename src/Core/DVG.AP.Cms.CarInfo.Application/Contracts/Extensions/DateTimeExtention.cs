using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVG.AP.Cms.CarInfo.Application.Contracts.Extensions
{
    public static class DateTimeExtention
    {
        public static long ToUnixTimeSeconds(this DateTime dateTime)
        {
            return (long)(new DateTimeOffset(dateTime).ToUnixTimeSeconds());
        }

        public static long DateTimeToUnixTime(this DateTime dateTime)
        {
            //DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            //TimeSpan span = (TimeSpan)(dateTime - epoch.ToLocalTime());
            //return (long)(span.TotalSeconds * 1000.0);
            return (long)(new DateTimeOffset(dateTime).ToUnixTimeMilliseconds() * 1000.0);
        }

        public static long DateTimeToUnixTimeDaily(DateTime dateTime)
        {
            dateTime = DateTime.Parse(dateTime.ToString("MM/dd/yyyy 00:00:00"));
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var span = (TimeSpan)(dateTime.Date - epoch.ToLocalTime());
            return (long)(span.TotalSeconds * 1000.0);
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp / 1000.0).ToLocalTime();
            return dtDateTime;
        }
    }
}
