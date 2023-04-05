using CurryFit.model.blocks;
using CurryFit.view;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;
using Xamarin.Forms;

namespace CurryFit.model.Sets
{
    public class SuperSet : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private double Aweight;
        public double AWeight
        {
            get { return Aweight; }
            set
            {

                Aweight = value;
                OnPropertyChanged("AWeight");


            }
        }
        private int Areps;
        public int AReps
        {
            get { return Areps; }
            set
            {

                Areps = value;
                OnPropertyChanged("AReps");


            }
        }

        private double Bweight;
        public double BWeight
        {
            get { return Bweight; }
            set
            {

                Bweight = value;
                OnPropertyChanged("BWeight");


            }
        }
        private int Breps;
        public int BReps
        {
            get { return Breps; }
            set
            {

                Breps = value;
                OnPropertyChanged("BReps");


            }
        }

        private bool AisVisible;
        public bool AIsVisible
        {
            get { return AisVisible; }
            set
            {

                AisVisible = value;
                OnPropertyChanged("AIsVisible");


            }
        }
        private bool BisVisible;
        public bool BIsVisible
        {
            get { return BisVisible; }
            set
            {

                BisVisible = value;
                OnPropertyChanged("BIsVisible");


            }
        }
        private string AisVisibleSource;
        public string AIsVisibleSource
        {
            get { return AisVisibleSource; }
            set
            {

                AisVisibleSource = value;
                OnPropertyChanged("AIsVisibleSource");


            }
        }
        private string BisVisibleSource;
        public string BIsVisibleSource
        {
            get { return BisVisibleSource; }
            set
            {

                BisVisibleSource = value;
                OnPropertyChanged("BIsVisibleSource");


            }
        }
        public string Title { get; set; }

        //Commands
        public ICommand IncrementAWeightCmd { get; private set; }
        public ICommand DecrementAWeightCmd { get; private set; }
        public ICommand IncrementBWeightCmd { get; private set; }
        public ICommand DecrementBWeightCmd { get; private set; }

        public ICommand IncrementARepsCmd { get; private set; }
        public ICommand DecrementARepsCmd { get; private set; }
        public ICommand IncrementBRepsCmd { get; private set; }
        public ICommand DecrementBRepsCmd { get; private set; }

        public ICommand UpdateASetVisibilityCmd { get; private set; }
        public ICommand UpdateBSetVisibilityCmd { get; private set; }




        [ForeignKey(typeof(SuperSetBlock))]
        public int SuperSetBlockId { get; set; }

        public SuperSet()
        {
            AWeight = 0;
            AReps = 0;
            BWeight = 0;
            BReps = 0;
            AIsVisible = true;
            BIsVisible = true;
            AIsVisibleSource = "pointer_up_gray.png";
            BIsVisibleSource = "pointer_up_gray.png";
            Title = "1";
            UpdateASetVisibilityCmd = new Command(() => { UpdateASetVisibility(); App.Database.UpdateSuperSetWithChildren(this); });
            UpdateBSetVisibilityCmd = new Command(() => { UpdateBSetVisibility(); App.Database.UpdateSuperSetWithChildren(this); });

            IncrementAWeightCmd = new Command(() => { AWeight++; App.Database.UpdateSuperSetWithChildren(this); });
            DecrementAWeightCmd = new Command(() => { AWeight--; App.Database.UpdateSuperSetWithChildren(this); });
            IncrementBWeightCmd = new Command(() => { BWeight++; App.Database.UpdateSuperSetWithChildren(this); });
            DecrementBWeightCmd = new Command(() => { BWeight--; App.Database.UpdateSuperSetWithChildren(this); });

            IncrementARepsCmd = new Command(() => { AReps++; App.Database.UpdateSuperSetWithChildren(this); });
            DecrementARepsCmd = new Command(() => { AReps--; App.Database.UpdateSuperSetWithChildren(this); });
            IncrementBRepsCmd = new Command(() => { BReps++; App.Database.UpdateSuperSetWithChildren(this); });
            DecrementBRepsCmd = new Command(() => { BReps--; App.Database.UpdateSuperSetWithChildren(this); });
        }

        public SuperSet(int c)
        {
            AWeight = 0;
            AReps = 0;
            BWeight = 0;
            BReps = 0;
            AIsVisible = true;
            BIsVisible = true;
            AIsVisibleSource = "pointer_up_gray.png";
            BIsVisibleSource = "pointer_up_gray.png";
            Title = c.ToString();
        }

        //Upadates the visability off set
        public SuperSet UpdateASetVisibility()
        {
            if (this.AIsVisible)
            {
                this.AIsVisible = false;
                this.AIsVisibleSource = "pointer_down_gray.png";
            }
            else
            {
                this.AIsVisible = true;
                this.AIsVisibleSource = "pointer_up_gray.png";
            }
            return this;
        }

        public SuperSet UpdateBSetVisibility()
        {
            if (this.BIsVisible)
            {
                this.BIsVisible = false;
                this.BIsVisibleSource = "pointer_down_gray.png";
            }
            else
            {
                this.BIsVisible = true;
                this.BIsVisibleSource = "pointer_up_gray.png";
            }
            return this;
        }
    }
}
