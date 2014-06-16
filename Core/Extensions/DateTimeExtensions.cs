using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            DateTime firstDayofNextMonth = FirstDayOfMonth(dt.AddMonths(1));
            return new DateTime(dt.Year, dt.Month, firstDayofNextMonth.AddDays(-1).Day);
        }

        public static int YearMonth(this DateTime dt)
        {
            return int.Parse(string.Format("{0}{1}", dt.Year, dt.Month));
        }

        public static int MonthDifference(this DateTime dt1, DateTime dt2)
        {
            return (dt1.Month - dt2.Month) + 12 * (dt1.Year - dt2.Year);
        }
    }
}
