using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.blocks
{
    public class NormalSetBlock
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsTextBlock { get; set; }

        // Dessa kan ersättas med "NormalSetBlockVisibility" osv
        public bool IsNormalSet { get; set; }
        public bool IsDropSet { get; set; }
        public bool IsSuperSet { get; set; }
        public bool IsEnduranceSet { get; set; }
        // -----------
        public int Order { get; set; }
        public string Title { get; set; }
        public string Fade1 { get; set; }
        public string Fade2 { get; set; }

        public bool NormalSetBlockVisibility { get; set; }

        // Test 
        public double XMargin { get; set; }
        public double Width { get; set; }
        public double GradientOffset { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        public int HoursSet { get; set; }
        public int MinutesSet { get; set; }
        public int SecondsSet { get; set; }

        public string TimerDisplay { get; set; }
        //--
        [OneToMany]
        public List<NormalSet> NormalSets { get; set; }

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; }

        public NormalSetBlock(int Counter)
        {
            IsTextBlock = false;
            IsNormalSet = true;
            IsDropSet = false;
            IsSuperSet = false;
            IsEnduranceSet = false;
            Order = Counter;
            Title = "NORMAL SET";
            Fade1 = "#A6A0A6";
            Fade2 = "#A6A0A6";
            NormalSetBlockVisibility = true;
            Width = 0;
            GradientOffset = 1.0;
            XMargin = 40;
            Hours = 0;
            Minutes = 0;
            Seconds = 30;
            HoursSet = 0;
            MinutesSet = 0;
            SecondsSet = 30;

            //Used to determine TimerDisplay
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
            TimerDisplay = hs + ":" + ms + ":" + ss;
        }

        public NormalSetBlock()
        {
            IsTextBlock = false;
            IsNormalSet = true;
            IsDropSet = false;
            IsSuperSet = false;
            IsEnduranceSet = false;
            Order = this.Order;
            Title = "NORMAL SET";
            Fade1 = this.Fade1;
            Fade2 = this.Fade2;
            NormalSetBlockVisibility = true;
            GradientOffset = this.GradientOffset;
            XMargin = this.XMargin;
            Hours = this.Hours;
            Minutes = this.Minutes;
            Seconds = this.Seconds;
            HoursSet = this.HoursSet;
            MinutesSet = this.MinutesSet;
            SecondsSet = this.SecondsSet;
        }

        public NormalSetBlock CloseAllSets()
        {
            foreach (NormalSet NS in this.NormalSets)
            {
                NS.IsVisible = false;
                NS.IsVisibleSource = "pointer_down_gray.png";
                App.Database.UpdateNormalSetWithChildren(NS);
            }
            return this;
        }

        public void UpdateNormalSetTitels(int i)
        {
            foreach (NormalSet ns in this.NormalSets)
            {
                if (int.Parse((ns.Title).Remove(0, 4)) > i)
                {
                    ns.Title = "SET " + (int.Parse((ns.Title).Remove(0, 4)) - 1).ToString();
                    App.Database.UpdateNormalSetWithChildren(ns);
                }
            }
        }

        public NormalSetBlock UpdateNormalSetBlockVisibility()
        {
            if (this.NormalSetBlockVisibility)
            {
                this.NormalSetBlockVisibility = false;
            }
            else
            {
                this.NormalSetBlockVisibility = true;
            }
            return this;
        }

    }
}
