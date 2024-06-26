using System;
using System.Collections.Generic;

namespace WebView
{
    public static class DateUtilities
    {
        public static List<DateTime> GetDateRangeArray(DateTime date, DateRangeType dateRangeType, DayOfWeek firstDayOfWeek, List<DayOfWeek>? workWeekDays = null, int daysToSelectInDayView = 1)
        {
            List<DateTime>? datesArray = new();
            DateTime startDate;
            DateTime endDate;

            if (workWeekDays == null)
            {
                workWeekDays = new List<DayOfWeek>() { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };
            }

            daysToSelectInDayView = Math.Max(daysToSelectInDayView, 1);

            switch (dateRangeType)
            {
                case DateRangeType.Day:
                    startDate = date.Date;
                    endDate = startDate.AddDays(daysToSelectInDayView);
                    break;

                case DateRangeType.Week:
                    startDate = GetStartDateOfWeek(date.Date, firstDayOfWeek);
                    endDate = startDate.AddDays(7);
                    break;
                case DateRangeType.WorkWeek:
                    startDate = GetStartDateOfWeek(date.Date, firstDayOfWeek);
                    endDate = startDate.AddDays(7);
                    break;

                case DateRangeType.Month:
                    startDate = new DateTime(date.Year, date.Month, 1);
                    endDate = startDate.AddMonths(1);
                    break;

                default:
                    throw new Exception("This should never be reached.");
            }

            // Populate the dates array with the dates in range
            DateTime nextDate = startDate;

            do
            {
                if (dateRangeType != DateRangeType.WorkWeek)
                {
                    // push all days not in work week view
                    datesArray.Add(nextDate);
                }
                else if (workWeekDays.IndexOf(nextDate.DayOfWeek) != -1)
                {
                    datesArray.Add(nextDate);
                }
                nextDate = nextDate.AddDays(1);
            } while (nextDate != endDate);

            return datesArray;
        }

        public static DateTime GetStartDateOfWeek(DateTime date, DayOfWeek firstDayOfWeek)
        {
            int daysOffset = firstDayOfWeek - date.DayOfWeek;
            if (daysOffset > 0)
            {
                // If first day of week is > date, go 1 week back, to ensure resulting date is in the past.
                daysOffset -= 7;
            }
            return date.AddDays(daysOffset);
        }


        public static List<int> GetWeekNumbersInMonth(int weeksInMonth, DayOfWeek firstDayOfWeek, FirstWeekOfYear firstWeekOfYear, DateTime navigatedDate)
        {
            int selectedYear = navigatedDate.Year;
            int selectedMonth = navigatedDate.Month;
            int daysInMonth = DateTime.DaysInMonth(selectedYear, selectedMonth);
            int dayOfMonth = 1;
            DateTime fistDayOfMonth = new(selectedYear, selectedMonth, dayOfMonth);
            DayOfWeek endOfFirstWeek = dayOfMonth + (firstDayOfWeek + 7 - 1) - AdjustWeekDay(firstDayOfWeek, fistDayOfMonth.DayOfWeek);
            DateTime endOfWeekRange = new(selectedYear, selectedMonth, (int)endOfFirstWeek);
            dayOfMonth = endOfWeekRange.Day;
            List<int>? weeksArray = new();
            for (int i = 0; i < weeksInMonth; i++)
            {
                // Get week number for end of week
                weeksArray.Add(GetWeekNumber(endOfWeekRange, firstDayOfWeek, firstWeekOfYear));
                dayOfMonth += 7;
                if (dayOfMonth > daysInMonth) dayOfMonth = daysInMonth;
                endOfWeekRange = new DateTime(selectedYear, selectedMonth, dayOfMonth);
            }
            return weeksArray;
        }

        public static int AdjustWeekDay(DayOfWeek firstDayOfWeek, DayOfWeek dateWeekDay)
        {
            return firstDayOfWeek != DayOfWeek.Sunday && dateWeekDay < firstDayOfWeek ? (int)dateWeekDay + 7 : (int)dateWeekDay;
        }

        public static int GetWeekNumber(DateTime date, DayOfWeek firstDayOfWeek, FirstWeekOfYear firstWeekOfYear)
        {
            // First four-day week of the year - minumum days count
            int fourDayWeek = 4;

            return firstWeekOfYear switch
            {
                FirstWeekOfYear.FirstFullWeek => GetWeekOfYearFullDays(date, firstDayOfWeek, 7),
                FirstWeekOfYear.FirstFourDayWeek => GetWeekOfYearFullDays(date, firstDayOfWeek, fourDayWeek),
                _ => GetFirstDayWeekOfYear(date, firstDayOfWeek),
            };
        }

        public static int GetFirstDayWeekOfYear(DateTime date, DayOfWeek firstDayOfWeek)
        {
            int num = date.DayOfYear - 1;
            DayOfWeek num2 = date.DayOfWeek - (num % 7);
            int num3 = ((int)num2 - (int)firstDayOfWeek + 2 * 7) % 7;

            return (num + num3) / 7 + 1;
        }

        public static int GetWeekOfYearFullDays(DateTime date, DayOfWeek firstDayOfWeek, int numberOfFullDays)
        {
            int dayOfYear = date.DayOfYear - 1;
            DayOfWeek num = date.DayOfWeek - (dayOfYear % 7);

            DateTime lastDayOfPrevYear = new(date.Year, 12, 31);
            int daysInYear = lastDayOfPrevYear.DayOfYear - 1;

            int num2 = (firstDayOfWeek - num + 2 * 7) % 7;
            if (num2 != 0 && num2 >= numberOfFullDays)
            {
                num2 -= 7;
            }

            int num3 = dayOfYear - num2;
            if (num3 < 0)
            {
                num -= daysInYear % 7;
                num2 = (firstDayOfWeek - num + 2 * 7) % 7;
                if (num2 != 0 && num2 + 1 >= numberOfFullDays)
                {
                    num2 -= 7;
                }

                num3 = daysInYear - num2;
            }

            return num3 / 7 + 1;
        }

    }
}
