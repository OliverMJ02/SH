using CurryFit.model.Sets;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CurryFit.model.blocks
{
    public class NormalSetBlock : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public bool IsNormalSet { get; set; }

        private bool normalSetBlockVisibility;
        public bool NormalSetBlockVisibility
        {
            get { return normalSetBlockVisibility; }
            set
            {

                normalSetBlockVisibility = value;
                OnPropertyChanged(nameof(NormalSetBlockVisibility));
            }
        }

        private bool isNotFinished;
        public bool IsNotFinished
        {
            get { return isNotFinished; }
            set
            {

                isNotFinished = value;
                OnPropertyChanged(nameof(IsNotFinished));
            }
        }

        private string saveOrFinish;
        public string SaveOrFinish
        {
            get { return saveOrFinish; }
            set
            {

                saveOrFinish = value;
                OnPropertyChanged(nameof(SaveOrFinish));
            }
        }

        public int Order { get; set; } // The order it's shown in the flow.
        public string Title { get; set; } // Title of the block i.e "NORMAL SET", "DROP SET" etc

        public string NumberOfSets { get; set; } //The number of sets the block has currently

        private double xMargin; // Used for resting timer animation
        public double XMargin
        {
            get { return xMargin; }
            set
            {

                xMargin = value;
                OnPropertyChanged(nameof(XMargin));
            }
        }
        private double width; // Used for resting timer animation
        public double Width
        {
            get { return width; }
            set
            {

                width = value;
                OnPropertyChanged(nameof(Width));
            }

        }

        // Active data for timer, will change when timer is active
        public int Hours { get; set; } 
        public int Minutes { get; set; }
        public int Seconds { get; set; }

        // Set data for timer, will not change until it's changed in the timer settings
        public int HoursSet { get; set; }
        public int MinutesSet { get; set; }
        public int SecondsSet { get; set; }

        public bool TimerOn { get; set; }

        private string timerDisplay;  // What the Resting timer will display
        public string TimerDisplay
        {
            get { return timerDisplay; }
            set
            {
                timerDisplay = value;
                OnPropertyChanged(nameof(TimerDisplay));
            }
        }
        //--
        [OneToMany]
        public List<NormalSet> NormalSets { get; set; } // List of sets connected to the block

        [ForeignKey(typeof(LogDay))]
        public int LogDayId { get; set; } //Id of the LogDay it belongs to

        public ICommand UpdateBlockVisibilityCmd { get; private set; }

        public ICommand HandleRestingTimerCmd { get; private set; }

        public ICommand HandleMinChangeCmd { get; private set; }

        public ICommand HandleSecChangeCmd { get; private set; }

        
        public NormalSetBlock()
        {
            IsNormalSet = this.IsNormalSet;
            IsNotFinished = this.IsNotFinished;
            SaveOrFinish = this.SaveOrFinish;
            Order = this.Order;
            Title = "NORMAL SET";
            NormalSetBlockVisibility = true;
            NumberOfSets = this.NumberOfSets;
            XMargin = this.XMargin;
            Hours = this.Hours;
            Minutes = this.Minutes;
            Seconds = this.Seconds;
            HoursSet = this.HoursSet;
            MinutesSet = this.MinutesSet;
            SecondsSet = this.SecondsSet;
            TimerOn = false;
            TimerDisplay = this.TimerDisplay;

            HandleMinChangeCmd = new Command<(int, int, IList<int>)>(tuple =>
            {
                var (selectedWheelIndex, indexOfItemChangedInSelectedWheel, selectedItemsIndexes) = tuple;
                Minutes = indexOfItemChangedInSelectedWheel;
                MinutesSet = indexOfItemChangedInSelectedWheel;
                TimerOn = false;
                Width = 0;
                XMargin = 40;
                App.Database.UpdateNormalBlockWithChildren(this);
            });

            HandleSecChangeCmd = new Command<(int, int, IList<int>)>(tuple =>
            {
                var (selectedWheelIndex, indexOfItemChangedInSelectedWheel, selectedItemsIndexes) = tuple;
                Seconds= indexOfItemChangedInSelectedWheel;
                SecondsSet = indexOfItemChangedInSelectedWheel;
                TimerOn = false;
                Width = 0;
                XMargin = 40;
                App.Database.UpdateNormalBlockWithChildren(this);
            });

            UpdateBlockVisibilityCmd = new Command(() => { UpdateNormalSetBlockVisibility(); IsNotFinished = false; SaveOrFinish = "SAVE CHANGES"; App.Database.UpdateNormalBlockWithChildren(this);});

            HandleRestingTimerCmd = new Command(() => {

                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                var deviceWidth = mainDisplayInfo.Width;
                var xamarinWidth = deviceWidth / mainDisplayInfo.Density;
                double startWidth = xamarinWidth - 26 - 35 - 26 - 5;

                
                model.Timer timer = new model.Timer(0, 0, 0);
                int time = Hours * 3600 + Minutes * 60 + Seconds;
                int c = 0;
                TimerOn = App.Database.GetNormalBlockWithChildren(this.Id).TimerOn;
                if (TimerOn)
                {
                    TimerOn = false;
                    App.Database.UpdateNormalBlockWithChildren(this);
                }
                else
                {
                    double barWidth = startWidth - Width;
                    timer.Hours = Hours;
                    timer.Minutes = Minutes;
                    timer.Seconds = Seconds;
                    TimerOn = true;
                    App.Database.UpdateNormalBlockWithChildren(this);
                    Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
                    {
                        try
                        {
                            Width = Width + barWidth / time/20;
                            XMargin = XMargin - barWidth / time/20;
                            if (c%20 == 0)
                            {
                                TimerOn = App.Database.GetNormalBlockWithChildren(this.Id).TimerOn;
                                timer.Update();
                                timer.UpdateDisplay();
                                TimerDisplay = timer.Display;
                                Hours = timer.Hours;
                                Minutes = timer.Minutes;
                                Seconds = timer.Seconds;
                                App.Database.UpdateNormalBlockWithChildren(this);
                            }

                            c++;
                        }
                        catch { TimerOn = false; }

                        if (time - c/20 < 1)
                        {
                            this.TimerOn = false;
                            Width = 0;
                            XMargin = 40;
                            Hours = HoursSet;
                            Minutes = MinutesSet;
                            Seconds = SecondsSet;
                            timer.Hours = HoursSet;
                            timer.Minutes = MinutesSet;
                            timer.Seconds = SecondsSet;
                            timer.UpdateDisplay();
                            TimerDisplay = timer.Display;
                            App.Database.UpdateNormalBlockWithChildren(this);
                            return false;
                        }

                        try { return App.Database.GetNormalBlockWithChildren(this.Id).TimerOn; }
                        catch { return false; }
                        
                    });
                }
            });
        }

        public NormalSetBlock(int Counter)
        {
            IsNormalSet = true;
            IsNotFinished = true;
            SaveOrFinish = "FINISH EXERCISE";
            Order = Counter;
            Title = "NORMAL SET";
            NormalSetBlockVisibility = true;
            NumberOfSets = "1 SET";
            Width = 0;
            XMargin = 40;
            Hours = 0;
            Minutes = 0;
            Seconds = 30;
            HoursSet = 0;
            MinutesSet = 0;
            SecondsSet = 30;
            TimerOn = false;


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
