using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace CurryFit.model
{
    /// <summary>
    /// Class for handling the base logic of a calendar, uses C# DateTime
    /// </summary>
    public class Calendar
    {
        private DateTime selectedDate;
        CultureInfo culture = new CultureInfo("en-SE", false);
        String format = "dd MMMM yyyy";
         

        /// <summary>
        /// Method for getting the next calendar day 
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns></returns
        public String Get_NextDay(String date)
        {
            var convertedDate = DateTime.Parse(date, culture);
            convertedDate = convertedDate.AddDays(1);
            selectedDate = convertedDate;
            return convertedDate.ToString(format);
        }

        /// <summary>
        /// Method for getting the previous calendar day
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns></returns>
        public String Get_PreviousDay(String date)
        {
            var convertedDate = DateTime.Parse(date, culture);
            convertedDate = convertedDate.AddDays(-1);
            selectedDate = convertedDate;
            return convertedDate.ToString(format);
        }

        /// <summary>
        /// Method for getting the selected date's weekday 
        /// </summary>
        /// <param name="date"> the date in DateTime format </param>
        /// <returns> The name of the weekday as a string </returns>
        public String Get_WeekDay(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }

        public TimeSpan Get_TimeSpan(DateTime date1, DateTime date2)
        {
            return date1.Subtract(date2);
        }

        
    }
}
