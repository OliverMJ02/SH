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
    public class NormalSet : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private double weight;
        public double Weight
        {
            get { return weight; }
            set
            {
                
                 weight = value;
                 OnPropertyChanged("Weight");
                
                
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
        public ICommand IncrementWeightCmd { get; private set; }
        public ICommand DecrementWeightCmd { get; private set; }

        public ICommand IncrementRepsCmd { get; private set; }
        public ICommand DecrementRepsCmd { get; private set; }

        public ICommand UpdateSetVisibilityCmd { get; private set; }




        [ForeignKey(typeof(NormalSetBlock))]
        public int NormalSetBlockId { get; set; }

        public NormalSet()
        {
            Weight= 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET 1";
            UpdateSetVisibilityCmd = new Command(() =>{ UpdateSetVisibility(); App.Database.UpdateNormalSetWithChildren(this); });
            IncrementWeightCmd = new Command(() => { Weight++; App.Database.UpdateNormalSetWithChildren(this); });
            DecrementWeightCmd = new Command(() => { Weight--; App.Database.UpdateNormalSetWithChildren(this); });

            IncrementRepsCmd = new Command(() => { Reps++; App.Database.UpdateNormalSetWithChildren(this);});

            DecrementRepsCmd = new Command(() => { Reps--; App.Database.UpdateNormalSetWithChildren(this); });
        }

        public NormalSet(int c)
        {
            Weight = 0;
            Reps = 0;
            IsVisible = true;
            IsVisibleSource = "pointer_up_gray.png";
            Title = "SET " + c.ToString();
        }

        //Upadates the visability off set
        public NormalSet UpdateSetVisibility()
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
