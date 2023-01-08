using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;
using CurryFit.model;
using CurryFit.model.blocks;
using Firebase.Database;
using Firebase.Database.Query;
using CurryFit.model.Sets;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        int CurrentDay = 2;
        LogDay currentLogDay;

        List<object> Blocks = new List<object>();
        FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://projectspice-shoof-default-rtdb.europe-west1.firebasedatabase.app/");
        public WorkoutPage()
        {
            InitializeComponent();

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;
            MenuLayout.WidthRequest = xamarinWidth;

            BtnHome.WidthRequest = xamarinWidth * 0.2;
            BtnFood.WidthRequest = xamarinWidth * 0.2;
            BtnWorkout.WidthRequest = xamarinWidth * 0.2;
            BtnStats.WidthRequest = xamarinWidth * 0.2;
            BtnProfile.WidthRequest = xamarinWidth * 0.2;

            SearchBar.WidthRequest = xamarinWidth * 0.6;
            FilterBtn.WidthRequest = xamarinWidth * 0.3;

        }
        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        void Handle_ToExercises(object sender, EventArgs e)
        {
            ExerciseLayout.IsVisible = true;
            LogbookLayout.IsVisible = false;
        }

        void Handle_ToLogbook(object sender, EventArgs e)
        {
            ExerciseLayout.IsVisible = false;
            LogbookLayout.IsVisible = true;

            List<LogDay> logDays = App.Database.GetLogDays();
            bool exists = false;
            foreach(LogDay logDay in logDays)
            {
                if(logDay.Day == CurrentDay)
                {
                    exists = true;
                    currentLogDay = logDay;
                    currentLogDay = App.Database.GetLogDayWithChildren(logDay.Id);
                    break;
                }
            }
            if (!exists)
            {
                currentLogDay = new LogDay{ 
                    Day = CurrentDay,
                    
                    NormalSetBlocks = new List<NormalSetBlock>() { },
                    DropSetBlocks = new List<DropSetBlock>() { },
                    
                    
                };
                App.Database.SaveLogDay(currentLogDay);
            }
            try
            {
                Blocks = currentLogDay.GetAllBlocks();
                BindableLayout.SetItemsSource(BlockCollection, null);
                BindableLayout.SetItemsSource(BlockCollection, Blocks);
            }
            catch { }
        }

        void Handle_NextDay(object sender, EventArgs e)
        {
            CurrentDay++;
            List<LogDay> logDays = App.Database.GetLogDays();
            bool exists = false;
            foreach (LogDay logDay in logDays)
            {
                if (logDay.Day == CurrentDay)
                {
                    exists = true;
                    currentLogDay = logDay;
                    currentLogDay = App.Database.GetLogDayWithChildren(logDay.Id);
                    break;
                }
            }
            if (!exists)
            {
                currentLogDay = new LogDay
                {
                    Day = CurrentDay,

                    NormalSetBlocks = new List<NormalSetBlock>() { },
                    DropSetBlocks = new List<DropSetBlock>() { },


                };
                App.Database.SaveLogDay(currentLogDay);
            }
            try
            {
                Blocks = currentLogDay.GetAllBlocks();
                BindableLayout.SetItemsSource(BlockCollection, null);
                BindableLayout.SetItemsSource(BlockCollection, Blocks);
            }
            catch { }
        }

        void Handle_PreviousDay(object sender, EventArgs e)
        {
            CurrentDay--;
            List<LogDay> logDays = App.Database.GetLogDays();
            bool exists = false;
            foreach (LogDay logDay in logDays)
            {
                if (logDay.Day == CurrentDay)
                {
                    exists = true;
                    currentLogDay = logDay;
                    currentLogDay = App.Database.GetLogDayWithChildren(logDay.Id);
                    break;
                }
            }
            if (!exists)
            {
                currentLogDay = new LogDay
                {
                    Day = CurrentDay,

                    NormalSetBlocks = new List<NormalSetBlock>() { },
                    DropSetBlocks = new List<DropSetBlock>() { },


                };
                App.Database.SaveLogDay(currentLogDay);
            }
            try
            {
                Blocks = currentLogDay.GetAllBlocks();
                BindableLayout.SetItemsSource(BlockCollection, null);
                BindableLayout.SetItemsSource(BlockCollection, Blocks);
            }
            catch { }
        }

        void Handle_AddSets(object sender, EventArgs e)
        {
            ChooseSetLayout.IsVisible = true;
        }
        
        void Handle_NewTextBlock(object sender, EventArgs e)
        {

            Blocks.Add(new TextBlock{ IsTextBlock = true, IsNormalSet = false, IsDropSet = false, IsSuperSet = false, IsEnduranceSet = false, Order = currentLogDay.Counter, Text="", Title="TEXT BLOCK"});
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, Blocks);
            currentLogDay.Counter++;
            App.Database.UpdateLogDay(currentLogDay);
        }

        //---------- NORMAL SETS ---------------

        void Handle_AddNormalSet(object sender, EventArgs e)
        {
            NormalSet ns = new NormalSet();
            App.Database.SaveNormalSet(ns);
            NormalSetBlock nsb = new NormalSetBlock(currentLogDay.Counter);
            App.Database.SaveNormalBlock(nsb);
            nsb = App.Database.GetNormalBlockWithChildren(nsb.Id);
            nsb.NormalSets.Add(App.Database.GetNormalSetWithChildren(ns.Id));
            App.Database.UpdateNormalBlockWithChildren(nsb);
            currentLogDay.NormalSetBlocks.Add(nsb);
            currentLogDay.Counter++;
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            Blocks.Add(nsb);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
            ChooseSetLayout.IsVisible = false;
        }

        void Handle_NewNormalSet(object sender, EventArgs e)
        {
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(((Button)sender).CommandParameter);
            NormalSet ns = new NormalSet (nsb.NormalSets.Count + 1);
            App.Database.SaveNormalSet(ns);
            nsb = nsb.CloseAllSets();
            nsb.NormalSets.Add(App.Database.GetNormalSetWithChildren(ns.Id));
            nsb.Fade1 = "#A6A0A6";
            nsb.Fade2 = "#A6A0A6";
            App.Database.UpdateNormalBlockWithChildren(nsb);
            App.Database.UpdateLogDayWithChildren(currentLogDay);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void UpdateNormalSetVisibility(object sender, EventArgs e)
        {
            NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
            ns.UpdateSetVisibility();
            App.Database.UpdateNormalSetWithChildren(ns);
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DeleteNormalSet(object sender, EventArgs e)
        {
            NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
            nsb.UpdateNormalSetTitels(int.Parse((ns.Title).Remove(0, 4)));
            App.Database.DeleteNormalSet(ns);
            //If there is no sets left, change Add new set button border to fade colors
            if(nsb.NormalSets.Count-1 == 0)
            {
                nsb.Fade1 = "#FF4816";
                nsb.Fade2 = "#FFE000";
                App.Database.UpdateNormalBlockWithChildren(nsb);
            }
            nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
            App.Database.UpdateLogDayWithChildren(currentLogDay);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DeleteNormalBlock(object sender, EventArgs e)
        {
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(((ImageButton)sender).CommandParameter);
            foreach (NormalSet ns in nsb.NormalSets)
            {
                App.Database.DeleteNormalSet(ns);
            }
            App.Database.DeleteNormalBlock(nsb);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_NormalSetDecreaseWeight(object sender, EventArgs e)
        {
            NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
            ns.Weight--;
            App.Database.UpdateNormalSetWithChildren(ns);
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }
        void Handle_NormalSetIncreaseWeight(object sender, EventArgs e)
        {
            NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
            ns.Weight++;
            App.Database.UpdateNormalSetWithChildren(ns);
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
 
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }
        
        void Handle_NormalSetUpdateWeight(object sender, EventArgs e)
        {
            try
            {
                NormalSet ns = App.Database.GetNormalSetWithChildren(((Entry)sender).Placeholder);

                try
                {
                    ns.Weight = Double.Parse(((Entry)sender).Text);
                }
                catch
                {
                    ns.Weight = 0;
                }
                
                App.Database.UpdateNormalSetWithChildren(ns);
                NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);

            }
            catch { }
        }
        void Handle_NormalSetDecreaseReps(object sender, EventArgs e)
        {
            NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
            ns.Reps--;
            App.Database.UpdateNormalSetWithChildren(ns);
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }
        void Handle_NormalSetIncreaseReps(object sender, EventArgs e)
        {
            NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
            ns.Reps++;
            App.Database.UpdateNormalSetWithChildren(ns);
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);

            bool check = true;
            foreach (NormalSet ns2 in nsb.NormalSets)
            {
                if (ns2.Id > ns.Id)
                {
                    check = false;
                    break;
                }
            }
            //Changes add new set button to faded border only if the last set was edited
            if (check && ns.Reps > 0)
            {
                nsb.Fade1 = "#FF4816";
                nsb.Fade2 = "#FFE000";
                App.Database.UpdateNormalBlockWithChildren(nsb);
            }

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_NormalSetUpdateReps(object sender, EventArgs e)
        {
            bool check = true;
            try
            {
                NormalSet ns = App.Database.GetNormalSetWithChildren(((Entry)sender).Placeholder);
                //We dont want to make add new set buttons border faded if we just focused and then unfocused without changing anything on reps
                try
                {
                    if (int.Parse(((Entry)sender).Text) == ns.Reps)
                    {
                        check = false;
                    }
                }
                catch { }

                try
                {
                    ns.Reps = int.Parse(((Entry)sender).Text);
                }
                catch
                {
                    ns.Reps = 0;
                }

                App.Database.UpdateNormalSetWithChildren(ns);
                NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
                
                foreach(NormalSet ns2 in nsb.NormalSets)
                {
                    if(ns2.Id > ns.Id)
                    {
                        check = false;
                        break;
                    }
                }
                //Changes add new set button to faded border only if the last set was edited
                if (check)
                {
                    nsb.Fade1 = "#FF4816";
                    nsb.Fade2 = "#FFE000";
                    App.Database.UpdateNormalBlockWithChildren(nsb);
                    BindableLayout.SetItemsSource(BlockCollection, null);
                    BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
                }
                
            }
            catch { }

        }

        //---------- DROP SETS ---------------
        void Handle_AddDropSet(object sender, EventArgs e)
        {
            DropSet ds = new DropSet();
            App.Database.SaveDropSet(ds);
            DropSetBlock dsb = new DropSetBlock(currentLogDay.Counter);
            App.Database.SaveDropBlock(dsb);
            dsb = App.Database.GetDropBlockWithChildren(dsb.Id);
            dsb.DropSets.Add(App.Database.GetDropSetWithChildren(ds.Id));
            App.Database.UpdateDropBlockWithChildren(dsb);
            Blocks.Add(dsb);
            currentLogDay.DropSetBlocks.Add(dsb);
            currentLogDay.Counter++;
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
            ChooseSetLayout.IsVisible = false;
        }

        void Handle_NewDropSet(object sender, EventArgs e)
        {
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(((Button)sender).CommandParameter);
            DropSet ds = new DropSet(dsb.DropSets.Count + 1);
            App.Database.SaveDropSet(ds);
            dsb = dsb.CloseAllSets();
            dsb.DropSets.Add(App.Database.GetDropSetWithChildren(ds.Id));
            dsb.Fade1 = "#A6A0A6";
            dsb.Fade2 = "#A6A0A6";
            App.Database.UpdateDropBlockWithChildren(dsb);
            App.Database.UpdateLogDayWithChildren(currentLogDay);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void UpdateDropSetVisibility(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.UpdateSetVisibility();
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DeleteDropSet(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);
            dsb.UpdateDropSetTitels(int.Parse((ds.Title).Remove(0, 4)));
            App.Database.DeleteDropSet(ds);
            //If there is no sets left, change Add new set button border to fade colors
            if (dsb.DropSets.Count - 1 == 0)
            {
                dsb.Fade1 = "#FF4816";
                dsb.Fade2 = "#FFE000";
                App.Database.UpdateDropBlockWithChildren(dsb);
            }
            dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);
            App.Database.UpdateLogDayWithChildren(currentLogDay);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DeleteDropBlock(object sender, EventArgs e)
        {
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(((ImageButton)sender).CommandParameter);
            foreach (DropSet ds in dsb.DropSets)
            {
                App.Database.DeleteDropSet(ds);
            }
            App.Database.DeleteDropBlock(dsb);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DropSetDecreaseStartWeight(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.StartWeight--;
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }
        void Handle_DropSetIncreaseStartWeight(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.StartWeight++;
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DropSetUpdateStartWeight(object sender, EventArgs e)
        {
            try
            {
                DropSet ds = App.Database.GetDropSetWithChildren(((Entry)sender).Placeholder);

                try
                {
                    ds.StartWeight = Double.Parse(((Entry)sender).Text);
                }
                catch
                {
                    ds.StartWeight = 0;
                }

                App.Database.UpdateDropSetWithChildren(ds);
                DropSetBlock nsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            }
            catch { }
        }


        void Handle_DropSetDecreaseEndWeight(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.EndWeight--;
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }
        void Handle_DropSetIncreaseEndWeight(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.EndWeight++;
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DropSetUpdateEndWeight(object sender, EventArgs e)
        {
            try
            {
                DropSet ds = App.Database.GetDropSetWithChildren(((Entry)sender).Placeholder);

                try
                {
                    ds.EndWeight = Double.Parse(((Entry)sender).Text);
                }
                catch
                {
                    ds.EndWeight = 0;
                }

                App.Database.UpdateDropSetWithChildren(ds);
                DropSetBlock nsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            }
            catch { }
        }


        void Handle_DropSetDecreaseReps(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.Reps--;
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }
        void Handle_DropSetIncreaseReps(object sender, EventArgs e)
        {
            DropSet ds = App.Database.GetDropSetWithChildren(((ImageButton)sender).CommandParameter);
            ds.Reps++;
            App.Database.UpdateDropSetWithChildren(ds);
            DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

            bool check = true;
            foreach (DropSet ds2 in dsb.DropSets)
            {
                if (ds2.Id > ds.Id)
                {
                    check = false;
                    break;
                }
            }
            //Changes add new set button to faded border only if the last set was edited
            if (check && ds.Reps > 0)
            {
                dsb.Fade1 = "#FF4816";
                dsb.Fade2 = "#FFE000";
                App.Database.UpdateDropBlockWithChildren(dsb);
            }

            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DropSetUpdateReps(object sender, EventArgs e)
        {
            bool check = true;
            try
            {
                DropSet ds = App.Database.GetDropSetWithChildren(((Entry)sender).Placeholder);
                //We dont want to make add new set buttons border faded if we just focused and then unfocused without changing anything on reps
                try
                {
                    if (int.Parse(((Entry)sender).Text) == ds.Reps)
                    {
                        check = false;
                    }
                }
                catch { }

                try
                {
                    ds.Reps = int.Parse(((Entry)sender).Text);
                }
                catch
                {
                    ds.Reps = 0;
                }

                App.Database.UpdateDropSetWithChildren(ds);
                DropSetBlock dsb = App.Database.GetDropBlockWithChildren(ds.DropSetBlockId);

                foreach (DropSet ds2 in dsb.DropSets)
                {
                    if (ds2.Id > ds.Id)
                    {
                        check = false;
                        break;
                    }
                }
                //Changes add new set button to faded border only if the last set was edited
                if (check)
                {
                    dsb.Fade1 = "#FF4816";
                    dsb.Fade2 = "#FFE000";
                    App.Database.UpdateDropBlockWithChildren(dsb);
                    BindableLayout.SetItemsSource(BlockCollection, null);
                    BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
                }
            }
            catch { }

        }

        //---------- SUPER SETS ---------------
        void Handle_AddSuperSet(object sender, EventArgs e)
        {
            Blocks.Add(new SuperSetBlock { IsTextBlock = false, IsNormalSet = false, IsDropSet = false, IsSuperSet = true, IsEnduranceSet = false, Order = currentLogDay.Counter, Title="SUPER SET" });
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, Blocks);
            ChooseSetLayout.IsVisible = false;
            currentLogDay.Counter++;
            App.Database.UpdateLogDayWithChildren(currentLogDay);
        }

        //---------- ENDURANCE SETS ---------------
        void Handle_AddEnduranceSet(object sender, EventArgs e)
        {
            Blocks.Add(new EnduranceSetBlock { IsTextBlock = false, IsNormalSet = false, IsDropSet = false, IsSuperSet = false, IsEnduranceSet = true, Order = currentLogDay.Counter, Title="ENDURANCE SET" });
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, Blocks);
            ChooseSetLayout.IsVisible = false;            
            currentLogDay.Counter++;
            App.Database.UpdateLogDayWithChildren(currentLogDay);
        }

        void Handle_Favorised(object sender, EventArgs e)
        {
            App.Database.GetSingleExercise((int)(sender as ImageButton).CommandParameter).UpdateFavorised();
            BindableLayout.SetItemsSource(ExerciseCollection, App.Database.GetExercises());
        }


        //Dessa genererar bara test exempel på övningar för att se hur det kan se ut. Kommer inte behövas sen.
        void Handle_AddNewExercise(object sender, EventArgs e)
        {
            
            Exercise ex = new Exercise();
            ex.Name = "Test namn";
            ex.Creator = "Strengthhub";
            ex.MainEquipment = "Machine";
            ex.MainMuscle = "Triceps";
            ex.FavorisedSource = "star_empty.png";
            //await firebaseClient.Child("Exercises").PostAsync(ex);
            //BindableLayout.SetItemsSource(ExerciseCollection, await GetAllExercises());
            App.Database.SaveExercise(ex);
            BindableLayout.SetItemsSource(ExerciseCollection, App.Database.GetExercises());
        }

        async void Handle_AddNewExercise2(object sender, EventArgs e)
        {
            Exercise ex = new Exercise();
            ex.Name = "Annat namn";
            ex.Creator = "Strengthhub";
            ex.MainEquipment = "Dumbells";
            ex.MainMuscle = "Biceps";
            ex.FavorisedSource = "star_empty.png";
            await firebaseClient.Child("Exercises").PostAsync(ex);
            //BindableLayout.SetItemsSource(ExerciseCollection, await GetAllExercises());
            //App.Database.SaveExercise(ex);
            BindableLayout.SetItemsSource(ExerciseCollection, App.Database.GetExercises());

        }

        void Handle_DeleteExercise(object sender, EventArgs e)
        {
            //await firebaseClient.Child("Exercises").Child((string)(sender as Button).CommandParameter).DeleteAsync();
            //BindableLayout.SetItemsSource(ExerciseCollection, await GetAllExercises());
            App.Database.DeleteSingleExercise((sender as Button).CommandParameter);
            BindableLayout.SetItemsSource(ExerciseCollection, App.Database.GetExercises());
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            /*
            try
            {
                trainingProgramsView.ItemsSource = App.Database.GetTrainingPrograms();
            }
            catch { }
            */
            
            try
            {
                BindableLayout.SetItemsSource(ExerciseCollection, App.Database.GetExercises());
            }
            catch { }
           
            //BindableLayout.SetItemsSource(ExerciseCollection, await GetAllExercises());
        }
/*
        private void Handle_TrainingPrograms(object sender, EventArgs e)
        {
            trainingProgramsView.IsVisible = true;
            exercisesView.IsVisible = false;
            LabelTrainingProgramsBox.IsVisible = true;
            LabelExcercisesBox.IsVisible = false;
            LabelCurrentBox.IsVisible = false;
            CreateProgramBtn.IsVisible = true;
            CreateExcerciseBtn.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = false;
            CurrentProgram.IsVisible = false;
            CurrentExcercise.IsVisible = false;
            CreateProgram.IsVisible = false;
            CreateExcercise.IsVisible = false;
            EditExcercise.IsVisible = false;
            EditProgram.IsVisible = false;
            BackToExcercises.CommandParameter = null;
            pex.isProgram = true;
            filterLayout.IsVisible = true;
        }

        private void Handle_Excercises(object sender, EventArgs e)
        {
            trainingProgramsView.IsVisible = false;
            exercisesView.IsVisible = true;
            LabelTrainingProgramsBox.IsVisible = false;
            LabelExcercisesBox.IsVisible = true;
            LabelCurrentBox.IsVisible = false;
            CreateProgramBtn.IsVisible = false;
            CreateExcerciseBtn.IsVisible = true;
            CurrentExcercise.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = false;
            CurrentProgram.IsVisible = false;
            CreateProgram.IsVisible = false;
            CreateExcercise.IsVisible = false;
            EditExcercise.IsVisible = false;
            EditProgram.IsVisible = false;
            BackToExcercises.CommandParameter = null;
            pex.isProgram = false;
            filterLayout.IsVisible = true;
        }

        private void Handle_Current(object sender, EventArgs e)
        {
            BackToExcercises.CommandParameter = null;
            EditExcercise.IsVisible = false;
            EditProgram.IsVisible = false;
        }

        void Handle_CreateExcercise(object sender, EventArgs e)
        {
            exercisesView.IsVisible = false;
            trainingProgramsView.IsVisible = false;
            CreateExcercise.IsVisible = true;
            CreateProgram.IsVisible = false;
            CreateProgramBtn.IsVisible = false;
            CreateExcerciseBtn.IsVisible = false;
            filterLayout.IsVisible = false;
        }
        void Handle_BackToExcercises(object sender, EventArgs e)
        {
            if (BackToExcercises.CommandParameter == null)
            {
                exercisesView.IsVisible = true;
                LabelTrainingProgramsBox.IsVisible = false;
                LabelExcercisesBox.IsVisible = true;
                LabelCurrentBox.IsVisible = false;
                filterLayout.IsVisible = true;
                CreateExcerciseBtn.IsVisible = true;
                CreateExcercise.IsVisible = false;
            }
            else
            {
                CurrentProgram.IsVisible = true;
                CurrentProgramDeleteButton.IsVisible = true;
            }
            trainingProgramsView.IsVisible = false;
            CurrentExcercise.IsVisible = false;

        }

        void Handle_BackToTrainingPrograms(object sender, EventArgs e)
        {
            exercisesView.IsVisible = false;
            trainingProgramsView.IsVisible = true;
            CreateExcercise.IsVisible = false;
            CreateProgram.IsVisible = false;
            CreateProgramBtn.IsVisible = true;
            CreateExcerciseBtn.IsVisible = false;
            CurrentExcercise.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = false;
            CurrentProgram.IsVisible = false;
            LabelTrainingProgramsBox.IsVisible = true;
            LabelExcercisesBox.IsVisible = false;
            LabelCurrentBox.IsVisible = false;
            BackToExcercises.CommandParameter = null;
            filterLayout.IsVisible = true;

        }

        async void Handle_SaveExcercise(object sender, EventArgs e)
        {
            bool failed = true;
            try
            {
                Exercise excercise = new Exercise()
                {
                    Name = CreateExcerciseName.Text,
                    Description = CreateExcerciseDescription.Text,
                    Difficulty = CreateExcerciseDifficulty.SelectedItem.ToString(),
                    MuscleGroup = CreateExcerciseMuscleGroup.Text,
                    VideoLink = CreateExcerciseVideoLink.Text,
                    Creator = "You",
                    Location = CreateExcerciseLocation.SelectedItem.ToString(),
                    FavorisedSource = "unfilledStar.png"
                };

                CreateExcerciseName.Text = "";
                CreateExcerciseDescription.Text = "Description";
                CreateExcerciseDifficulty.SelectedItem = "";
                CreateExcerciseMuscleGroup.Text = "";
                CreateExcerciseVideoLink.Text = "";
                CreateExcerciseLocation.SelectedItem = "";

                App.Database.SaveExercises(excercise);
                exercisesView.ItemsSource = App.Database.GetExercises();

                exercisesView.IsVisible = true;
                trainingProgramsView.IsVisible = false;
                CreateExcercise.IsVisible = false;
                CreateProgram.IsVisible = false;
                CreateProgramBtn.IsVisible = false;
                CreateExcerciseBtn.IsVisible = true;
                filterLayout.IsVisible = true;

                failed = false;
            }
            catch { }
            if (failed)
            {
                await DisplayAlert("Error", "Please fill all fields", "OK");
            }

        }


        async void Handle_SaveProgram(object sender, EventArgs e)
        {
            bool failed = true;
            try
            {


                TrainingProgram trainingProgram = new TrainingProgram()
                {
                    Name = CreateProgramName.Text,
                    Description = CreateProgramDescription.Text,
                    Difficulty = CreateProgramDifficulty.SelectedItem.ToString(),
                    MuscleGroups = CreateProgramMuscleGroups.Text,
                    Location = CreateProgramLocation.SelectedItem.ToString(),
                    Creator = "You",
                    FavorisedSource = "unfilledStar.png"
                };

                CreateProgramName.Text = "";
                CreateProgramDescription.Text = "Description";
                CreateProgramDifficulty.SelectedItem = "";
                CreateProgramMuscleGroups.Text = "";
                CreateProgramLocation.SelectedItem = "";

                App.Database.SaveTrainingProgram(trainingProgram);

                try
                {
                    trainingProgram.Exercises = ExercisesList;
                    App.Database.UpdateTrainingProgramWithChildren(trainingProgram);
                }
                catch { }

                ExercisesList.Clear();
                trainingProgramsView.ItemsSource = App.Database.GetTrainingPrograms();
                collectionCurrentProgram.ItemsSource = App.Database.GetExercises();

                exercisesView.IsVisible = false;
                trainingProgramsView.IsVisible = true;
                CreateExcercise.IsVisible = false;
                CreateProgram.IsVisible = false;
                CreateProgramBtn.IsVisible = true;
                CreateExcerciseBtn.IsVisible = false;
                filterLayout.IsVisible = true;

                failed = false;
            }
            catch { }
            if (failed)
            {
                await DisplayAlert("Error", "Please fill all fields", "OK");
            }

        }

        async void Handle_CreateProgram(object sender, EventArgs e)
        {
            exercisesView.IsVisible = false;
            trainingProgramsView.IsVisible = false;
            CreateExcercise.IsVisible = false;
            CreateProgram.IsVisible = true;
            CreateProgramBtn.IsVisible = false;
            CreateExcerciseBtn.IsVisible = false;
            filterLayout.IsVisible = false;

            ExercisesList.Clear();
            collectionviewAddExcercise.ItemsSource = App.Database.GetExercises();

            await CreateProgramScrollView.ScrollToAsync(0, 0, false);
        }


        void Handle_AddExcerciseToProgram(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;
            if (btn.Text.Equals("Remove Excercise"))
            {
                List<Exercise> list = new List<Exercise>();
                foreach (Exercise ex in ExercisesList)
                {
                    if (ex.Id != int.Parse(id.ToString()))
                    {
                        list.Add(ex);
                    }
                }
                //ExcercisesList.Remove(App.TrainingProgramsDB.GetSingleExcerciseAsync(id));
                ExercisesList = list;

                btn.TextColor = Color.DeepSkyBlue;
                btn.Text = "Add Excercise";

            }
            else
            {
                ExercisesList.Add(App.Database.GetSingleExercise(id));
                btn.TextColor = Color.Red;
                btn.Text = "Remove Excercise";
            }

        }


        void Handle_ShowCurrentProgram(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;
            var item = (TapGestureRecognizer)stack.GestureRecognizers[0];
            object id = item.CommandParameter;

            BackToExcercises.CommandParameter = id;

            TrainingProgram trainingProgram = App.Database.GetSingleTrainingProgram(id);
            CurrentProgramName.Text = trainingProgram.Name;
            CurrentProgramDescription.Text = trainingProgram.Description;
            CurrentProgramDifficulty.Text = trainingProgram.Difficulty;
            CurrentProgramMuscleGroups.Text = trainingProgram.MuscleGroups;
            CurrentProgramLocation.Text = trainingProgram.Location;
            CurrentExcerciseCreator.Text = trainingProgram.Creator;

            collectionCurrentProgram.ItemsSource = trainingProgram.Exercises;

            CurrentProgramDeleteButton.CommandParameter = trainingProgram.Id;
            EditProgramBtn.CommandParameter = trainingProgram.Id;

            CurrentProgram.IsVisible = true;
            exercisesView.IsVisible = false;
            trainingProgramsView.IsVisible = false;
            CreateExcerciseBtn.IsVisible = false;
            CreateProgramBtn.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = true;
            filterLayout.IsVisible = false;


            LabelTrainingProgramsBox.IsVisible = false;
            LabelExcercisesBox.IsVisible = false;
            LabelCurrentBox.IsVisible = true;

        }


        void Handle_ShowCurrentExcercise(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;
            var item = (TapGestureRecognizer)stack.GestureRecognizers[0];
            object id = item.CommandParameter;

            Exercise exercise = App.Database.GetSingleExercise(id);
            CurrentExcerciseName.Text = exercise.Name;
            CurrentExcerciseDescription.Text = exercise.Description;
            CurrentExcerciseDifficulty.Text = "Difficulty: " + exercise.Difficulty;
            CurrentExcerciseMuscleGroup.Text = "Muscle groups: " + exercise.MuscleGroup;
            CurrentExcerciseVideoLink.Source = exercise.VideoLink;
            CurrentExcerciseCreator.Text = "Created by: " + exercise.Creator;
            CurrentExcerciseLocation.Text = "Location: " + exercise.Location;

            CurrentDeleteButton.CommandParameter = exercise.Id;
            EditExcercisebtn.CommandParameter = exercise.Id;

            CurrentExcercise.IsVisible = true;
            exercisesView.IsVisible = false;
            trainingProgramsView.IsVisible = false;
            CreateExcerciseBtn.IsVisible = false;
            filterLayout.IsVisible = false;

            LabelTrainingProgramsBox.IsVisible = false;
            LabelExcercisesBox.IsVisible = false;
            LabelCurrentBox.IsVisible = true;

            CurrentProgram.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = false;

        }


        async void Handle_DeleteExcercise(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Warning!", "Are you sure that you want to delete this excercise?", "Yes", "No");
            if (answer)
            {
                var btn = sender as Button;
                object id = btn.CommandParameter;

                App.Database.DeleteSingleExercise(id);
                exercisesView.ItemsSource = App.Database.GetExercises();

                CurrentExcercise.IsVisible = false;
                exercisesView.IsVisible = true;
                trainingProgramsView.IsVisible = false;
                CreateExcerciseBtn.IsVisible = true;

                LabelTrainingProgramsBox.IsVisible = false;
                LabelExcercisesBox.IsVisible = true;
                LabelCurrentBox.IsVisible = false;

                BackToExcercises.CommandParameter = null;
                filterLayout.IsVisible = true;
            }

        }

        async void Handle_DeleteProgram(object sender, EventArgs e)
        {

            bool answer = await DisplayAlert("Warning!", "Are you sure that you want to delete this training program?", "Yes", "No");
            if (answer)
            {
                var btn = sender as Button;
                object id = btn.CommandParameter;

                App.Database.DeleteSingleTrainingProgram(id);
                trainingProgramsView.ItemsSource = App.Database.GetTrainingPrograms();

                CurrentProgram.IsVisible = false;
                trainingProgramsView.IsVisible = true;
                exercisesView.IsVisible = false;
                CreateProgramBtn.IsVisible = true;
                CurrentProgramDeleteButton.IsVisible = false;

                LabelTrainingProgramsBox.IsVisible = true;
                LabelExcercisesBox.IsVisible = false;
                LabelCurrentBox.IsVisible = false;

                BackToExcercises.CommandParameter = null;
                filterLayout.IsVisible = true;
            }


        }
        void Handle_EditExcercise(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;

            Exercise exercise = App.Database.GetSingleExercise(id);

            EditExcercise.IsVisible = true;
            CurrentExcercise.IsVisible = false;

            EditExcerciseDifficulty.Title = "Difficulty";

            EditExcerciseDescription.Text = exercise.Description;
            EditExcerciseDifficulty.SelectedItem = exercise.Difficulty;
            EditExcerciseLocation.SelectedItem = exercise.Location;
            EditExcerciseMuscleGroup.Text = exercise.MuscleGroup;
            EditExcerciseName.Text = exercise.Name;
            EditExcerciseVideoLink.Text = exercise.VideoLink;

            EditExcerciseSave.CommandParameter = id;

        }

        void Handle_SaveEditedExcercise(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;

            EditExcercise.IsVisible = false;
            CurrentExcercise.IsVisible = true;

            Exercise exercise = App.Database.GetSingleExercise(id);
            exercise.Description = EditExcerciseDescription.Text;
            exercise.Difficulty = EditExcerciseDifficulty.SelectedItem.ToString();
            exercise.MuscleGroup = EditExcerciseMuscleGroup.Text;
            exercise.Location = EditExcerciseLocation.SelectedItem.ToString();
            exercise.Name = EditExcerciseName.Text;
            exercise.VideoLink = EditExcerciseVideoLink.Text;

            CurrentExcerciseName.Text = exercise.Name;
            CurrentExcerciseDescription.Text = exercise.Description;
            CurrentExcerciseDifficulty.Text = exercise.Difficulty;
            CurrentExcerciseMuscleGroup.Text = exercise.MuscleGroup;
            CurrentExcerciseVideoLink.Source = exercise.VideoLink;
            CurrentExcerciseCreator.Text = exercise.Creator;
            CurrentExcerciseLocation.Text = exercise.Location;

            App.Database.UpdateExercise(exercise);
            exercisesView.ItemsSource = App.Database.GetExercises();
        }

        async void Handle_EditProgram(object sender, EventArgs e)
        {
            await EditProgramScrollView.ScrollToAsync(0, 0, false);

            var btn = sender as Button;
            object id = btn.CommandParameter;

            TrainingProgram trainingProgram = App.Database.GetSingleTrainingProgram(id);

            EditProgramDifficulty.Title = "Difficulty";

            EditProgramDescription.Text = trainingProgram.Description;
            EditProgramDifficulty.SelectedItem = trainingProgram.Difficulty;
            EditProgramLocation.SelectedItem = trainingProgram.Location;
            EditProgramMuscleGroups.Text = trainingProgram.MuscleGroups;
            EditProgramName.Text = trainingProgram.Name;

            EditProgram.IsVisible = true;
            CurrentProgram.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = false;

            foreach (Exercise exercise in App.Database.GetExercises())
            {
                foreach (Exercise exercise2 in trainingProgram.Exercises)
                {
                    if (exercise2.Id == exercise.Id)
                    {
                        exercise.btnAddOrRemove = "Remove Excercise";
                        exercise.btnColor = "Red";
                        App.Database.UpdateExercise(exercise);
                        break;
                    }
                    else
                    {
                        exercise.btnAddOrRemove = "Add Excercise";
                        exercise.btnColor = "DeepSkyBlue";
                        App.Database.UpdateExercise(exercise);
                    }
                }

            }

            collectionviewEditExcercise.ItemsSource = App.Database.GetExercises();

            SaveProgramBtnEdit.CommandParameter = id;
        }

        void Handle_SaveEditedProgram(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;

            EditProgram.IsVisible = false;
            CurrentProgram.IsVisible = true;
            CurrentProgramDeleteButton.IsVisible = true;


            TrainingProgram program = App.Database.GetSingleTrainingProgram(id);
            program.Description = EditProgramDescription.Text;
            program.Difficulty = EditProgramDifficulty.SelectedItem.ToString();
            program.MuscleGroups = EditProgramMuscleGroups.Text;
            program.Location = EditProgramLocation.SelectedItem.ToString();
            program.Name = EditProgramName.Text;

            CurrentProgramName.Text = program.Name;
            CurrentProgramDescription.Text = program.Description;
            CurrentProgramDifficulty.Text = program.Difficulty;
            CurrentProgramMuscleGroups.Text = program.MuscleGroups;
            CurrentProgramCreator.Text = program.Creator;
            CurrentProgramLocation.Text = program.Location;

            App.Database.UpdateTrainingProgram(program);
            trainingProgramsView.ItemsSource = App.Database.GetTrainingPrograms();
            collectionCurrentProgram.ItemsSource = program.Exercises;
        }

        void Handle_EditExcerciseToProgram(object sender, EventArgs e)
        {
            object idProgram = SaveProgramBtnEdit.CommandParameter;

            var btn = sender as Button;
            object id = btn.CommandParameter;
            Exercise exercise = App.Database.GetSingleExercise(id);
            TrainingProgram program = App.Database.GetSingleTrainingProgram(idProgram);

            if (btn.Text.Equals("Remove Exercise"))
            {
                List<Exercise> list = new List<Exercise>();
                foreach (Exercise ex in program.Exercises)
                {
                    if (ex.Id != exercise.Id)
                    {
                        list.Add(ex);
                    }
                }
                program.Exercises = list;
                App.Database.UpdateTrainingProgramWithChildren(program);
                btn.TextColor = Color.DeepSkyBlue;
                btn.Text = "Add Exercise";

            }
            else
            {
                program.Exercises.Add(exercise);
                App.Database.UpdateTrainingProgramWithChildren(program);
                btn.TextColor = Color.Red;
                btn.Text = "Remove Exercise";
            }
        }

        void Handle_UpdateProgramStar(object sender, EventArgs e)
        {
            var ImgBtn = sender as ImageButton;
            object id = ImgBtn.CommandParameter;

            try
            {
                TrainingProgram program = App.Database.GetSingleTrainingProgram(id);

                if (program.isFavorised)
                {
                    program.isFavorised = false;
                    program.FavorisedSource = "unfilledStar.png";
                    ImgBtn.Source = "unfilledStar.png";
                    App.Database.UpdateTrainingProgramWithChildren(program);
                }
                else
                {
                    program.isFavorised = true;
                    program.FavorisedSource = "filledStar.png";
                    ImgBtn.Source = "filledStar.png";
                    App.Database.UpdateTrainingProgramWithChildren(program);
                }
            }

            catch { }

            //Behövs för att Excercise view ska bli updaterad med korrekta ex även vid filtrering, annars så blir det så att om man trycker på "stjärnan" och sedan direkt väljer att filtrera favoriserade så blir det fel då
            //Då filtering med favoriserade hänvisar till de som lär i itemsourcen redan och de är inte updaterade utan denna kod. Det blir lite omständigt eftersom man vill ju att "favorit filtreringen"
            //Också ska ta hänsyn till tidigare filtering och det går därför inte att bara kopiera in en alla excercises till "ItemSouce" för då tar den med excercises som kanske inte var med
            //i den tidigare filteringen. Väldigt rörigt men det funkar nu, Kanske går att lösa snyggare sen.
            List<TrainingProgram> tpstemp = App.Database.GetTrainingPrograms();
            List<TrainingProgram> tps = trainingProgramsView.ItemsSource as List<TrainingProgram>;
            List<TrainingProgram> tpsNew = new List<TrainingProgram>();
            foreach (TrainingProgram tp in tpstemp)
            {
                foreach (TrainingProgram tp2 in tps)
                {
                    if (tp.Id == tp2.Id)
                    {
                        tpsNew.Add(tp);
                    }
                }
            }
            trainingProgramsView.ItemsSource = tpsNew;


        }

        void Handle_UpdateExcerciseStar(object sender, EventArgs e)
        {
            var ImgBtn = sender as ImageButton;
            object id = ImgBtn.CommandParameter;

            try
            {
                Exercise exercise = App.Database.GetSingleExercise(id);

                if (exercise.isFavorised)
                {
                    exercise.isFavorised = false;
                    exercise.FavorisedSource = "unfilledStar.png";
                    ImgBtn.Source = "unfilledStar.png";
                    App.Database.UpdateExercise(exercise);
                }
                else
                {
                    exercise.isFavorised = true;
                    exercise.FavorisedSource = "filledStar.png";
                    ImgBtn.Source = "filledStar.png";
                    App.Database.UpdateExercise(exercise);
                }
            }

            catch { }

            //Behövs för att Excercise view ska bli updaterad med korrekta ex även vid filtrering, annars så blir det så att om man trycker på "stjärnan" och sedan direkt väljer att filtrera favoriserade så blir det fel då
            //Då filtering med favoriserade hänvisar till de som lär i itemsourcen redan och de är inte updaterade utan denna kod. Det blir lite omständigt eftersom man vill ju att "favorit filtreringen"
            //Också ska ta hänsyn till tidigare filtering och det går därför inte att bara kopiera in en alla excercises till "ItemSouce" för då tar den med excercises som kanske inte var med
            //i den tidigare filteringen. Väldigt rörigt men det funkar nu, Kanske går att lösa snyggare sen.
            List<Exercise> exstemp = App.Database.GetExercises();
            List<Exercise> exs = exercisesView.ItemsSource as List<Exercise>;
            List<Exercise> exsNew = new List<Exercise>();
            foreach (Exercise ex in exstemp)
            {
                foreach (Exercise ex2 in exs)
                {
                    if (ex.Id == ex2.Id)
                    {
                        exsNew.Add(ex);
                    }
                }
            }
            exercisesView.ItemsSource = exsNew;
        }


        //Filters excercises and training programs based on favorised or not

        void Handle_filterFavorised(object sender, EventArgs e)
        {
            if (filterStar.CommandParameter.ToString().Equals("false"))
            {
                List<TrainingProgram> tps = new List<TrainingProgram>();
                List<TrainingProgram> tpsTemp = (List<TrainingProgram>)trainingProgramsView.ItemsSource;
                foreach (TrainingProgram tp in tpsTemp)
                {
                    if (tp.isFavorised)
                    {
                        tps.Add(tp);
                    }
                }
                trainingProgramsView.ItemsSource = tps;

                List<Exercise> exs = new List<Exercise>();
                List<Exercise> exsTemp = (List<Exercise>)exercisesView.ItemsSource;
                foreach (Exercise ex in exsTemp)
                {
                    if (ex.isFavorised)
                    {
                        exs.Add(ex);
                    }
                }
                exercisesView.ItemsSource = exs;

                filterStar.CommandParameter = "true";
                filterStar.Source = "filledStar.png";
            }

            else
            {
                List<TrainingProgram> tps = new List<TrainingProgram>();
                List<TrainingProgram> tpsTemp = App.Database.GetTrainingPrograms();
                foreach (TrainingProgram tp in tpsTemp)
                {
                    if ((tp.Creator.Equals(pex.TpCreator) || pex.TpCreator.Equals("All")) &&
                        (tp.Difficulty.Equals(pex.TpDifficulty) || pex.TpDifficulty.Equals("All")) &&
                        (tp.Location.Equals(pex.TpLocation) || pex.TpLocation.Equals("All")) &&
                        (tp.MuscleGroups.Equals(pex.TpMuscleGroups) || pex.TpMuscleGroups.Equals("All")))
                    {
                        tps.Add(tp);
                    }
                }

                List<Exercise> exs = new List<Exercise>();
                List<Exercise> exsTemp = App.Database.GetExercises();
                foreach (Exercise ex in exsTemp)
                {
                    if ((ex.Creator.Equals(pex.ExCreator) || pex.ExCreator.Equals("All")) &&
                        (ex.Difficulty.Equals(pex.ExDifficulty) || pex.ExDifficulty.Equals("All")) &&
                        (ex.Location.Equals(pex.ExLocation) || pex.ExLocation.Equals("All")) &&
                        (ex.MuscleGroup.Equals(pex.ExMuscleGroup) || pex.ExMuscleGroup.Equals("All")))
                    {
                        exs.Add(ex);
                    }
                }

                trainingProgramsView.ItemsSource = tps;
                exercisesView.ItemsSource = exs;

                filterStar.CommandParameter = "false";
                filterStar.Source = "unfilledStar.png";
            }

        }


        async void Handle_ShowFilter(Object sender, EventArgs e)
        {
            /*
            filterStar.CommandParameter = "false";
            filterStar.Source = "unfilledStar.png";

            ProgramExcercise newPex = await Navigation.ShowPopupAsync(new WorkoutFilterPopup(pex));

            List<TrainingProgram> tps = new List<TrainingProgram>();
            List<TrainingProgram> tpsTemp = App.TrainingProgramsDB.GetTrainingProgramsAsync();
            foreach (TrainingProgram tp in tpsTemp)
            {
                if ((tp.Creator.Equals(newPex.TpCreator) || newPex.TpCreator.Equals("All")) &&
                    (tp.Difficulty.Equals(newPex.TpDifficulty) || newPex.TpDifficulty.Equals("All")) &&
                    (tp.Location.Equals(newPex.TpLocation) || newPex.TpLocation.Equals("All")) &&
                    (tp.MuscleGroups.Equals(newPex.TpMuscleGroups) || newPex.TpMuscleGroups.Equals("All")))
                {
                    tps.Add(tp);
                }
            }

            List<Excercise> exs = new List<Excercise>();
            List<Excercise> exsTemp = App.TrainingProgramsDB.GetExcercisesAsync();
            foreach (Excercise ex in exsTemp)
            {
                if ((ex.Creator.Equals(newPex.ExCreator) || newPex.ExCreator.Equals("All")) &&
                    (ex.Difficulty.Equals(newPex.ExDifficulty) || newPex.ExDifficulty.Equals("All")) &&
                    (ex.Location.Equals(newPex.ExLocation) || newPex.ExLocation.Equals("All")) &&
                    (ex.MuscleGroup.Equals(newPex.ExMuscleGroup) || newPex.ExMuscleGroup.Equals("All")))
                {
                    exs.Add(ex);
                }
            }

            trainingProgramsView.ItemsSource = tps;
            exercisesView.ItemsSource = exs;
            pex = newPex;
            
        
        }
    */
        }
    }