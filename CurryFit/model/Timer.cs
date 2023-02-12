using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CurryFit.model
{
    public class Timer
    {
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string Display { get; set; }

        public Timer(int h, int m, int s)
        {
            Hours = h;
            Minutes = m;
            Seconds = s;
            string hs;
            string ms;
            string ss;
            if (h <= 9)
            {
                hs = '0' + h.ToString();
            }
            else
            {
                hs = h.ToString();
            }
            if (m <= 9)
            {
                ms = '0' + m.ToString();
            }
            else
            {
                ms = m.ToString();
            }
            if (s <= 9)
            {
                ss = '0' + s.ToString();
            }
            else
            {
                ss = s.ToString();
            }
            Display = hs + ":" + ms + ":" + ss;
        }
        public void Update()
        {
            if (Seconds == 0)
            {
                if (Minutes == 0)
                {
                    if (Hours == 0)
                    {

                    }
                    else
                    {
                        Minutes = 59;
                        Seconds = 59;
                        Hours--;
                    }
                }
                else
                {
                    Minutes--;
                    Seconds = 59;
                }

            }
            else
            {
                Seconds--;
            }
        }

        public void UpdateDisplay()
        {
            string hs;
            string ms;
            string ss;
            if (Hours <= 9)
            {
                hs = '0' + Hours.ToString();
            }
            else
            {
                hs = Hours.ToString();
            }
            if (Minutes <= 9)
            {
                ms = '0' + Minutes.ToString();
            }
            else
            {
                ms = Minutes.ToString();
            }
            if (Seconds <= 9)
            {
                ss = '0' + Seconds.ToString();
            }
            else
            {
                ss = Seconds.ToString();
            }
            Display = hs + ":" + ms + ":" + ss;
        }
    }
}
