using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model
{
    /// <summary>
    /// Class for handling the base logic of a calendar
    /// </summary>
    public class Calendar
    {
        private int currentDate;
        private String format = "ddMMyyyy";

        /// <summary>
        /// Method for getting the next calendar day 
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns></returns
        public String get_NextDay(String date)
        {
            DateTime.TryParseExact(date, format, null,
                System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime convertedDate);
            convertedDate = convertedDate.AddDays(1);
            return convertedDate.ToString("d MM yyyy");
        }

        /// <summary>
        /// Method for getting the previous calendar day
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns></returns>
        public String get_PreviousDay(String date)
        {
            DateTime.TryParseExact(date, format, null,
                System.Globalization.DateTimeStyles.AdjustToUniversal, out DateTime convertedDate);
            convertedDate = convertedDate.AddDays(1);
            return convertedDate.ToString("d MM yyyy");
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
