using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillManager.Client.Enums;
using BillManager.Client.Model;
using Core.Extensions;

namespace BillManager.Client.Helpers
{
    public class BillHelper
    {
        public static DateTime Now
        {
            get { return DateTime.Now; }
        }
        public static Tuple<bool, DateTime> DetermineIsBillThisMonth(Bill bill)
        {
            DateTime currentMonthBill = ValidateDate(Now.Year, Now.Month, bill.DayDueInMonth.Value);
            switch (bill.BillFrequency)
            {
                case BillFrequency.Monthly:
                    return new Tuple<bool, DateTime>(true, currentMonthBill);
                case BillFrequency.BiMonthly:
                    if ((bill.CommenceDate.Month % 2 == 0) == (DateTime.Today.Month % 2 == 0))
                        return new Tuple<bool, DateTime>(true, currentMonthBill);
                    else
                    {
                        DateTime nextMonth = DateTime.Today.AddMonths(1);
                        return new Tuple<bool, DateTime>(false, ValidateDate(nextMonth.Year, nextMonth.Month, bill.DayDueInMonth.Value));
                    }
                case BillFrequency.SemiYearly:

                    if (bill.CommenceDate.Month == DateTime.Today.Month || bill.CommenceDate.AddMonths(6).Month == DateTime.Today.Month)
                        return new Tuple<bool, DateTime>(true, currentMonthBill);
                    else
                    {
                        DateTime sixMonths = bill.CommenceDate.AddMonths(6);
                        DateTime first = ValidateDate(Now.Year, bill.CommenceDate.Month, bill.DayDueInMonth.Value);
                        DateTime second = ValidateDate(sixMonths.Year, sixMonths.Month, bill.DayDueInMonth.Value);

                        if (Now.Date < first && (Now.Date.MonthDifference(first) >= 6))
                            return new Tuple<bool, DateTime>(false, first);
                        else if (Now.Date < second && (Now.Date.MonthDifference(second) >= 6))
                            return new Tuple<bool, DateTime>(false, second);
                        else
                        {
                            DateTime lastYearSecondBill = bill.CommenceDate.AddYears(-1).AddMonths(6);
                            return new Tuple<bool, DateTime>(false,
                                       ValidateDate(lastYearSecondBill.Year, lastYearSecondBill.Month, lastYearSecondBill.Day));
                        }
                    }
                case BillFrequency.Yearly:
                    if (bill.CommenceDate.Month == Now.Month)
                        return new Tuple<bool, DateTime>(true, currentMonthBill);
                    else if (bill.CommenceDate.Month < Now.Month)
                    {
                        DateTime nextYear = bill.CommenceDate.AddYears(1);
                        return new Tuple<bool, DateTime>(false, ValidateDate(nextYear.Year, nextYear.Month, bill.DayDueInMonth.Value));
                    }
                    else
                        return new Tuple<bool, DateTime>(false, currentMonthBill);
                default:
                    return new Tuple<bool, DateTime>(true, currentMonthBill);
            }
        }

        public static DateTime ValidateDate(int year, int month, int day)
        {
            DateTime date;
            while (!DateTime.TryParse(string.Format("{0}/{1}/{2}", month, day, year), out date))
                day--;

            return date;
        }
    }
}





//DateTime first = ValidateDate(DateTime.Now.Year, bill.CommenceDate.Month, bill.DayDueInMonth.Value);                        

//IDictionary<int, DateTime> quantList = new Dictionary<int, DateTime>();
//IList<int> sort = new List<int>();
//quantList.Add(DateTime.Today.YearMonth(), DateTime.Today.Date);
//sort.Add(DateTime.Today.YearMonth());
//quantList.Add(first.YearMonth(), first);
//sort.Add(first.YearMonth());
//quantList.Add(second.YearMonth(), second);
//sort.Add(second.YearMonth());
//quantList.OrderBy(k => k.Value);
//sort.OrderBy(k => k);

//int index = sort.IndexOf(DateTime.Today.YearMonth());

//if (index != sort.Count())
//    return new Tuple<bool, DateTime>(false, quantList[sort[index + 1]]);
//else
//    return new Tuple<bool, DateTime>(false, quantList[sort[0]].AddYears(1));