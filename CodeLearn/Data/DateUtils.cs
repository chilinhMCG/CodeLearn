using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLearn.Data
{
    public static class DateUtils
    {
        public static string RelativeTimeCompareToNow(DateTime date, string justNowText = "Vừa xong")
        {
            var diff = DateTime.Now.Subtract(date);

            if (diff.TotalSeconds < 60)
            {
                if (diff.TotalSeconds < 5)
                    return justNowText;

                else
                    return $"{(int)diff.TotalSeconds} giây trước";
            }
            else if (diff.TotalMinutes < 60)
            {
                return $"{(int)diff.TotalMinutes} phút trước";
            }
            else if (diff.TotalHours < 24)
            {
                return $"{(int)diff.TotalHours} giờ trước";
            }
            else if (diff.TotalDays < 31)
            {
                return $"{(int)diff.TotalDays} ngày trước";
            }
            else
            {
                int yearCount = (int)diff.TotalDays / 365;

                if (yearCount > 0)
                {
                    return $"{yearCount} năm trước";
                }
                else
                {
                    int monthCount = ((int)diff.TotalDays - (yearCount * 365)) / 31;
                    return $"{monthCount} tháng trước";
                }
            }

        }

        public static string ToShortDateStringDMY(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
