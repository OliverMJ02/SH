using CurryFit.model.blocks;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CurryFit.model.Sets
{
    public class DropSet : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private double startWeight;
        public double StartWeight
        {
            get { return startWeight; }
            set
            {

                startWeight = value;
                OnPropertyChanged("StartWeight");


            }
        }

        private double endWeight;
        public double EndWeight
        {
            get { return endWeight; }
            set
            {

                endWeight = value;
                OnPropertyChanged("EndWeight");


            }
        }
        private int reps;
        public int Reps
        {
            get { return reps; }
            set
            {

                reps = value;
                OnPropertyChanged("Reps");


            }
        }
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {

                isVisible = value;
                OnPropertyChanged("IsVisible");


            }
        }
        private string isVisibleSource;
        public string IsVisibleSource
        {
            get { return isVisibleSource; }
            set
            {

                isVisibleSource = value;
                OnPropertyChanged("IsVisibleSource");


            }
        }
        public string Title { get; set; }

        //Commands
        public ICommand IncrementStartWeightCmd { get; private set; }
        public ICommand DecrementStartWeightCmd { get; private set; }

        public ICommand IncrementEndWeightCmd { get; private set; }
        public ICommand DecrementEndWeightCmd { get; private set; }

        public ICommand IncrementRepsCmd { get; private set; }
        public ICommand DecrementRepsCmd { get; private set; }

        public ICommand UpdateSetVisibilityCmd { get; private set; }

        [ForeignKey(typeof(DropSetBlock))]
        public int DropSetBlockId { get; set; }


        public DropSet()
        {
            StartWeight = 0;
            EndWeight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET 1";
            UpdateSetVisibilityCmd = new Command(() => { UpdateSetVisibility(); App.Database.UpdateDropSetWithChildren(this); });
            IncrementStartWeightCmd = new Command(() => { StartWeight++; App.Database.UpdateDropSetWithChildren(this); });
            DecrementStartWeightCmd = new Command(() => { StartWeight--; App.Database.UpdateDropSetWithChildren(this); });
            IncrementEndWeightCmd = new Command(() => { EndWeight++; App.Database.UpdateDropSetWithChildren(this); });
            DecrementEndWeightCmd = new Command(() => { EndWeight--; App.Database.UpdateDropSetWithChildren(this); });

            IncrementRepsCmd = new Command(() => { Reps++; App.Database.UpdateDropSetWithChildren(this); });

            DecrementRepsCmd = new Command(() => { Reps--; App.Database.UpdateDropSetWithChildren(this); });
        }

        public DropSet(int c)
        {
            StartWeight = 0;
            EndWeight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET " + c.ToString();
        }

        public DropSet UpdateSetVisibility()
        {
            if (this.IsVisible)
            {
                this.IsVisible = false;
                this.IsVisibleSource = "pointer_down_gray.png";
            }
            else
            {
                this.IsVisible = true;
                this.IsVisibleSource = "pointer_up_gray.png";
            }
            return this;
        }
    }
}
