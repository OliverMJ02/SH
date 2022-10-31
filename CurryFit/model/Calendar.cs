using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace CurryFit.model
{
    /// <summary>
    /// Class for handling the base logic of a calendar
    /// </summary>
    public class Calendar
    {
        private int currentDate;
        CultureInfo culture = new CultureInfo("en-EN", false);
        String format = "dd MMMM yyyy";
         

        /// <summary>
        /// Method for getting the next calendar day 
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns></returns
        public String get_NextDay(String date)
        {
            var convertedDate = DateTime.Parse(date, culture);
            convertedDate = convertedDate.AddDays(1);
            return convertedDate.ToString(format);
        }

        /// <summary>
        /// Method for getting the previous calendar day
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns></returns>
        public String get_PreviousDay(String date)
        {
            var convertedDate = DateTime.Parse(date, culture);
            convertedDate = convertedDate.AddDays(-1);
            return convertedDate.ToString(format);
        }

        /// <summary>
        /// Method for getting the selected date's weekday 
        /// </summary>
        /// <param name="date"> the date in DateTime format </param>
        /// <returns> The name of the weekday as a string </returns>
        public String get_WeekDay(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }
        
    }
}
