using CurryFit.model.blocks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace CurryFit.model
{
    public class Timer
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public string Display { get; set; }

        public int PresetOrder { get; set; }
        public bool PresetMenuVisible {get; set;}
        public bool IsPreset { get; set; }

        [ForeignKey(typeof(Settings))]
        public int SettingsId { get; set; }


        public Timer()
        {
            Hours = this.Hours;
            Minutes = this.Minutes;
            Seconds = this.Seconds;
            Display = this.Display;
            SettingsId = this.SettingsId;
            PresetMenuVisible = this.PresetMenuVisible;
            PresetOrder = this.PresetOrder;
            IsPreset= this.IsPreset;
        }

        public Timer(int h, int m, int s)
        {
            Hours = h;
            Minutes = m;
            Seconds = s;
            IsPreset = true;
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

        public void UpdateDisplayWithoutHours()
        {
            string ms;
            string ss;

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
            Display =ms + " : " + ss;
        }
    }
}
