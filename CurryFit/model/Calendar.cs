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
        CultureInfo culture = CultureInfo.CurrentCulture;
        readonly string format = "dd MMMM yyyy";
         
        /// <summary>
        /// Constructor for the calendar class
        /// </summary>
        public Calendar()
        {
            selectedDate = DateTime.Today;
        }

        /// <summary>
        /// Method for getting the next calendar day
        /// </summary>
        /// <param name="date"> the date as a string </param>
        /// <returns>The calendar day after "date"</returns>
        public string Get_NextDay(string date)
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
        /// <returns>The calendar day previous to "date"</returns>
        public string Get_PreviousDay(string date)
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
        public string Get_WeekDay(DateTime date)
        {
            return date.DayOfWeek.ToString();
        }

        /// <summary>
        /// Method for getting the time span between two dates
        /// </summary>
        /// <param name="date1"> The first date </param>
        /// <param name="date2"> The second date </param>
        /// <returns> The time span between the two dates </returns>
        public TimeSpan Get_TimeSpan(DateTime date1, DateTime date2)
        {
            return date1.Subtract(date2);
        }

        
    }
}
