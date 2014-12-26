//////////////////////////////////////////////////////////////////////////////////////
// Author				: Shukri Adams												//
// Contact				: shukri.adams@gmail.com									//
//																					//
// vcFramework : A reuseable library of utility classes                             //
// Copyright (C)																	//
//////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Globalization;

namespace vcFramework.Time
{
    /// <summary>
    /// Static library of time-related functions.
    /// </summary>
	public static class DateTimeLib
	{
        /// <summary>
        /// Gets the exact datetime of a point of the day, relative to starting time. 
        /// Use this to find the datetime value of, for example, midnight before a given time.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static DateTime PreviousFixedTime(DateTime time, int hour) 
        {
            DateTime dt = new DateTime(time.Year, time.Month, time.Day, hour, 0, 0);
            if (time.Hour < hour)
                dt = dt.AddHours(-24);
            return dt;
        }

        public static string AgeFromSeconds(int seconds) 
        {
            if (seconds < 60)
                return "under a minute";
            if (seconds < 120)
                return "about a minute";
            if (seconds < 3600)
                return Math.Round((double)(seconds / 60), 0).ToString() + " minutes";

            return Math.Round((double)(seconds / 3600), 0).ToString() + " hours";
        }

		/// <summary>
		/// Gets the age, in seconds of a date. In other words, how "old" a date is.
		/// </summary>
		/// <param name="dateTimeUtc"></param>
        public static long AgeInSeconds(DateTime dateTimeUtc)
        {
            TimeSpan ts = DateTime.UtcNow - dateTimeUtc;
            if (ts.TotalSeconds > 1000000)
                return 1000000;
            return (long)Math.Round(ts.TotalSeconds, 0);
        }

		/// <summary> Returns ISO-formatted now datetime 
		/// </summary>
		/// <returns></returns>
		public static string DateNow(
			)
		{
			return DateNow(DateFormats.ISO);
		}

        /// <summary>
        /// Gets week of year. Specific to Norway, but at least it's consistent
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int GetWeek(DateTime date)
        {
            /*
            CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("no");
            Calendar cal = cult_info.Calendar;
            int weekCount = cal.GetWeekOfYear(date, cult_info.DateTimeFormat.CalendarWeekRule, cult_info.DateTimeFormat.FirstDayOfWeek);
            return weekCount;
            */
            DateTime start = new DateTime(date.Year, 1, 1);
            int days = (int)Math.Round((date - start).TotalDays, 0);

            if (days % 7 == 0)
                return (days / 7) -1;
            
            return (days / 7);
        }

        public static DateTime FirstDayOfWeek(int year, int weekNum)
        {
            DateTime start = new DateTime(year, 1, 1);
            return start.AddDays(7*weekNum);
            /*
            if (weekNum < 1)
                throw new Exception("Week number cannot be less than 1.");

            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);

            if (firstMonday.DayOfWeek != DayOfWeek.Monday)
                throw new Exception("First day of week must be a monday");

            var cal = CultureInfo.CurrentCulture.Calendar;
            CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("no");
            int firstWeek = cal.GetWeekOfYear(firstMonday, cult_info.DateTimeFormat.CalendarWeekRule, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            DateTime result = firstMonday.AddDays(weekNum * 7);

            return result;
             */ 
        }

        public static DateTime LastDayOfWeek(int year, int weekNum)
        {
            DateTime dt = FirstDayOfWeek(year, weekNum);
            dt = dt.AddDays(7);
            return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
        }

        /// <summary>
        /// From  http://stackoverflow.com/questions/662379/calculate-date-from-week-number
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekNum"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        static public DateTime WeekToDate(int weekcode)
        {
            CultureInfo cult_info = System.Globalization.CultureInfo.CreateSpecificCulture("no");
            CalendarWeekRule rule = cult_info.DateTimeFormat.CalendarWeekRule;

            int year = Int32.Parse(weekcode.ToString().Substring(0, 4));
            int weekNum = Int32.Parse(weekcode.ToString().Substring(4, 2));

            DateTime jan1 = new DateTime(year, 1, 1);

            int daysOffset = DayOfWeek.Monday - jan1.DayOfWeek;
            DateTime firstMonday = jan1.AddDays(daysOffset);

            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstMonday, rule, DayOfWeek.Monday);

            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            DateTime result = firstMonday.AddDays(weekNum * 7);
            return result;
        }

		/// <summary>Returns a string formatted datetime, 
		/// in one of three formats. </summary>
		/// <param name="DateTimeFormat"></param>
		/// <returns></returns>
		public static string DateNow(
			DateFormats dateTimeFormat
			)
		{
			//strDateType can be "USA", "STD" or "ISO"
			string strDay;
			string strMonth;
			string strOutput = "";
    

			strDay = System.DateTime.Now.Day.ToString();
			
			if (strDay.Length == 1)
				strDay = "0" + strDay;
			
			strMonth = System.DateTime.Now.Month.ToString();
			
			if (strMonth.Length == 1)
				strMonth = "0" + strMonth;

            switch (dateTimeFormat)
			{
				case DateFormats.ISO:
					strOutput = System.DateTime.Now.Year.ToString() + "-" + strMonth + "-" + strDay + " " + System.DateTime.Now.ToLongTimeString();
					break;
				case DateFormats.European:
					strOutput = strDay + "-" + strMonth + "-" + System.DateTime.Now.Year.ToString() + " " + System.DateTime.Now.ToLongTimeString();
					break;
				case DateFormats.US:
					strOutput = strMonth + "-" + strDay + "-" + System.DateTime.Now.Year.ToString() + " " + System.DateTime.Now.ToLongTimeString();
					break;
			}


		return strOutput;
		}

        
        /// <summary>
        /// Parses a string iso date into a date. expected format is 
        /// yyyy-mm-dd hh:mm:ss
        /// 0123456789012345678
        /// 
        /// </summary>
        /// <param name="isodate"></param>
        /// <returns></returns>
        public static DateTime ParseIso(string isodate)
        {
            int year = Int32.Parse(isodate.Substring(0, 4));
            int month = Int32.Parse(isodate.Substring(5, 2));
            int day = Int32.Parse(isodate.Substring(8, 2));
            int hour = Int32.Parse(isodate.Substring(11, 2));
            int minute = Int32.Parse(isodate.Substring(14, 2));
            int second = Int32.Parse(isodate.Substring(17, 2));

            return new DateTime(year, month, day, hour, minute, second);
        }
		
        /// <summary>
        /// Converts a datetime to unixtime
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static long ToUnixTime(DateTime date)
        {
            TimeSpan ts = (date - new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)Math.Round(ts.TotalSeconds, 0);
        }

        /// <summary>
        /// Converts a unix time time stamp into a .net time
        /// </summary>
        /// <param name="unixtimestamp"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long unixtimestamp)
        { 
            return new DateTime(1970,1,1,0,0,0).AddSeconds(unixtimestamp);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="date"></param>
		/// <returns></returns>
		public static string IsoDate(
			DateTime date
			)
		{
			string day;
			string month;

            day = date.Day.ToString();

            if (day.Length == 1)
                day = "0" + day;

            month = date.Month.ToString();

            if (month.Length == 1)
                month = "0" + month;

            return date.Year.ToString() + "-" + month + "-" + day + " " + date.ToLongTimeString();
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shortIso"></param>
        /// <returns></returns>
        public static DateTime ShortIsoDateToDate(int shortIso)
        {
            // yyyymmdd
            string date = shortIso.ToString();
            
            string day = date.Substring(6, 2);
            string month = date.Substring(4, 2);
            string year = date.Substring(0, 4);

            return new DateTime(int.Parse(year), int.Parse(month), int.Parse(day));
        }

        public static string IsoDateShort(
            DateTime date
            )
        {
            string day;
            string month;

            day = date.Day.ToString();

            if (day.Length == 1)
                day = "0" + day;

            month = date.Month.ToString();

            if (month.Length == 1)
                month = "0" + month;

            return date.Year.ToString() + "-" + month + "-" + day;
        }

        /// <summary>
        /// Converts a date into a numeric iso date consists of only year, month and date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static int IsoDateShortasInt(DateTime date)
        {
            string day;
            string month;

            day = date.Day.ToString();

            if (day.Length == 1)
                day = "0" + day;

            month = date.Month.ToString();

            if (month.Length == 1)
                month = "0" + month;

            return Int32.Parse((date.Year.ToString() + month + day));
        }

        /// <summary>
        /// Converts a datetime to javascript millseconds
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static double ToJavascriptMilliseconds(DateTime date, bool isUtc)
        {
            if (isUtc)
                return date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
               .TotalMilliseconds;

            return (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds;
            
        }

        /// <summary>
        /// Converts a date into a human-friendly "age" representation. Value is relative to now. Increments are :
        ///  X years, a year, X days, a day, x hours, an hour, X minutes, a minute, less than a minute.
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string Age(DateTime date)
        {
            TimeSpan ts = DateTime.Now - date;

            if (ts.TotalDays > (364 * 2))
            {
                int years = (int)Math.Round(ts.TotalDays / 364, 0);
                return string.Format("{0} years", years);
            }
            else if (ts.TotalDays > 364)
            {
                return string.Format("a year");
            }
            else if (ts.TotalDays > 1)
            {
                int days = (int)Math.Round(ts.TotalDays, 0);
                if (days > 1)
                    return string.Format("{0} days", days);
                else
                    return "a day";
            }
            else if (ts.TotalHours > 1)
            {
                int hours = (int)Math.Round(ts.TotalHours, 0);
                if (hours > 1)
                    return string.Format("{0} hours", hours);
                else
                    return "an hour";
            }
            else if (ts.TotalMinutes > 1)
            {
                int minutes = (int)Math.Round(ts.TotalMinutes, 0);
                if (minutes > 1)
                    return string.Format("{0} minutes", minutes);
                else
                    return string.Format("a minute", minutes);

            }
            else
                return "under a minute";
        }

        public static DateTime RoundToHour(DateTime d, bool roundForward)
        {
            int modifier = roundForward ? 1 : -1;

            // round hours
            if (d.Millisecond != 0)
            {
                d = d.AddMilliseconds(-d.Millisecond);
                d = d.AddSeconds(modifier * 1);
            }
            if (d.Second != 0)
            {
                d = d.AddSeconds(-d.Second);
                d = d.AddMinutes(modifier * 1);
            }
            if (d.Minute != 0)
            {
                d = d.AddMinutes(-d.Minute);
                d = d.AddHours(modifier * 1);
            }

            return d;
        }

        /// <summary>
        /// Converts a date to a human-friendly "when" representation. Value is relative to now.
        /// Values are "November,  1974", "X days ago", "yesterday", "X hours ago", "an hour ago", etc.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="ago"></param>
        /// <returns></returns>
        public static string When(DateTime date)
        {
            TimeSpan ts = DateTime.Now - date;

            if (ts.TotalDays > 31)
            {
                string month = date.ToString("MMMM");
                return string.Format("{0}, {1}", month, date.Year);
            }
            else if (ts.TotalDays > 1)
            {
                int days = (int)Math.Round(ts.TotalDays, 0);
                if (days > 1)
                    return string.Format("{0} days ago", days);
                else
                    return "yesterday";
            }
            else if (ts.TotalHours > 1)
            {
                int hours = (int)Math.Round(ts.TotalHours, 0);
                if (hours > 1)
                    return string.Format("{0} hours ago", hours);
                else
                    return "an hour ago";
            }
            else if (ts.TotalMinutes > 1)
            {
                int minutes = (int)Math.Round(ts.TotalMinutes, 0);
                if (minutes > 1)
                    return string.Format("{0} minutes ago", minutes);
                else
                    return "a minute ago";
            }
            else
                return "less than a minute ago";
        }
	}
}
