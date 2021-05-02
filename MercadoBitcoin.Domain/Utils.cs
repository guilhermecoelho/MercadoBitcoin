using System;

namespace MercadoBitcoin.Domain
{
    public class Utils
    {
        public string BuildUrl(string url, string coin, string method)
        {
            return $"{url}/{coin}/{method}";
        }

        public string BuildUrl(string url, string coin, string method, double from, double to = 0)
        {
            return to == 0 ? $"{url}/{coin}/{method}/{from}" : $"{url}/{coin}/{method}/{from}/{to}";
        }

        public string BuildUrl(string url, string coin, string method, DateTime dateTime)
        {
            return $"{url}/{coin}/{method}/{dateTime.Year}/{dateTime.Month}/{dateTime.Day}";
        }

        public string BuildUrlWithTid(string url, string coin, string method, double tid)
        {
            return $"{url}/{coin}/{method}/?tid={tid}";
        }


        public DateTime ConverterTimestampToDatetime(double timestamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);

            return dateTime;
        }

        public double ConvertDateTimeToTimeStamp(DateTime dateTime)
        {
            var datetime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            var timestamp = (double)span.TotalSeconds;

            return timestamp;
        }
    }
}
