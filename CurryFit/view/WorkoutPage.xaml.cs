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
using System.Threading;
using System.Globalization;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        int CurrentDay = 2;
        LogDay currentLogDay;
        double startWidth;


        NormalSetBlock currentNormalSetBlock;

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

            startWidth = xamarinWidth - 26 - 35 - 26 - 5;

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

        }

        int currentSec = 0;
        int currentMin = 0;
        void Handle_ToFilterView(object sender, EventArgs e)
        {
            currentNormalSetBlock = App.Database.GetNormalBlockWithChildren((sender as Button).CommandParameter);

            FilterView.BindingContext = currentNormalSetBlock;
            FilterView.IsVisible = true;
            List<string> list = new List<string>();
            for(int i = 0; i < 60; i++)
            {
                if(i < 10)
                {
                    list.Add("0"+i.ToString());
                }
                else
                {
                    list.Add(i.ToString());
                }
                
            }
            WheelPickerMinutes.ItemsSourceSimple = list;
            WheelPickerMinutes.SelectedItemsIndex = new List<int>() { currentNormalSetBlock.Minutes };
            WheelPickerSeconds.ItemsSourceSimple = list;
            WheelPickerSeconds.SelectedItemsIndex = new List<int>() { currentNormalSetBlock.Seconds };
            currentMin = currentNormalSetBlock.Minutes;
            currentSec = currentNormalSetBlock.Seconds;
            BindableLayout.SetItemsSource(PresetsView, App.Database.GetSettings().PresetTimers);  // Fills PresetsView with saved presets fromm settings (global presets)

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
        
        // --------- TextBlocks ----------
        void Handle_NewTextBlock(object sender, EventArgs e)
        {
            TextBlock tb = new TextBlock();
            tb.TextBlockVisibility = true;
            tb.IsEditing = true;
            tb.HasText = false;
            tb.Order = currentLogDay.Counter;
            tb.Title = "TEXT BLOCK";
            App.Database.SaveTextBlock(tb);
            currentLogDay.TextBlocks.Add(tb);
            currentLogDay.Counter++;
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
            
        }

        void Handle_DeleteTextBlock(object sender, EventArgs e)
        {
            TextBlock tb = App.Database.GetTextBlock(((ImageButton)sender).CommandParameter);
            currentLogDay.TextBlocks.Remove(tb);
            App.Database.DeleteTextBlock(tb.Id);
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            currentLogDay = App.Database.GetLogDayWithChildren(currentLogDay.Id);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());

        }

        // --------- ToDoLists -----------------

        void Handle_NewToDoList(object sender, EventArgs e)
        {
            ToDoList tdl = new ToDoList() { ToDoListVisibility = true, Title = "TO DO LIST", Order = currentLogDay.Counter };
            App.Database.SaveToDoList(tdl);
            currentLogDay.ToDoLists.Add(tdl);
            currentLogDay.Counter++;
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DeleteToDoList(object sender, EventArgs e)
        {
            ToDoList tdl = App.Database.GetToDoList(((ImageButton)sender).CommandParameter);
            currentLogDay.ToDoLists.Remove(tdl);
            foreach(ToDoItem tdi in tdl.ToDoItems)
            {
                App.Database.DeleteToDoItem(tdi.Id);
            }
            App.Database.DeleteToDoList(tdl.Id);
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            currentLogDay = App.Database.GetLogDayWithChildren(currentLogDay.Id);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_AddToDoItem(object sender, EventArgs e)
        {
            ToDoItem tdi = new ToDoItem() { IsEditing = true, HasText = false, CheckMarked = false};
            App.Database.SaveToDoItem(tdi);
            ToDoList tdl = App.Database.GetToDoList(((Button)sender).CommandParameter);
            tdl.ToDoItems.Add(App.Database.GetToDoItem(tdi.Id));
            App.Database.UpdateToDoList(tdl);
            App.Database.UpdateLogDayWithChildren(currentLogDay);
            currentLogDay = App.Database.GetLogDayWithChildren(currentLogDay.Id);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
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
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
            ChooseSetLayout.IsVisible = false;
        }

        void Handle_NewNormalSet(object sender, EventArgs e)
        {
            try
            {
                NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(((Button)sender).CommandParameter);
                NormalSet ns = new NormalSet(nsb.NormalSets.Count + 1);
                App.Database.SaveNormalSet(ns);
                nsb = nsb.CloseAllSets();
                nsb.NormalSets.Add(App.Database.GetNormalSetWithChildren(ns.Id));
                if (nsb.NormalSets.Count == 1)
                {
                    nsb.NumberOfSets = "1 SET";
                }
                else
                {
                    nsb.NumberOfSets = nsb.NormalSets.Count.ToString() + " SETS";
                }
                App.Database.UpdateNormalBlockWithChildren(nsb);
                App.Database.UpdateLogDayWithChildren(currentLogDay);

                BindableLayout.SetItemsSource(BlockCollection, null);
                BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
            }
            catch { }
        }
        
        void Handle_UpdateNormalSetBlockVisibility(object sender, EventArgs e)
        {
            NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(((ImageButton)sender).CommandParameter);
            nsb.UpdateNormalSetBlockVisibility();
            App.Database.UpdateNormalBlockWithChildren(nsb);
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());

        }

        void Handle_DeleteNormalSet(object sender, EventArgs e)
        {
            try
            {
                NormalSet ns = App.Database.GetNormalSetWithChildren(((ImageButton)sender).CommandParameter);
                NormalSetBlock nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
                if(nsb.NormalSets.Count > 1) 
                {
                    nsb.UpdateNormalSetTitels(int.Parse((ns.Title).Remove(0, 4)));
                    App.Database.DeleteNormalSet(ns);

                    if (nsb.NormalSets.Count == 1)
                    {
                        nsb.NumberOfSets = "1 SET";
                    }
                    else
                    {
                        nsb.NumberOfSets = nsb.NormalSets.Count.ToString() + " SETS";
                    }
                    App.Database.UpdateNormalBlockWithChildren(nsb);
                    nsb = App.Database.GetNormalBlockWithChildren(ns.NormalSetBlockId);
                    App.Database.UpdateLogDayWithChildren(currentLogDay);

                    BindableLayout.SetItemsSource(BlockCollection, null);
                    BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
                }
                
            }
            catch { }
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

        //Filter Functions

        void Handle_Test(object sender, EventArgs e)
        {
            if(t1.Border.Thickness == 2)
            {
                t1.Border.Thickness = 0;
                t2.Border.Thickness = 2;
            }
            else
            {
                t2.Border.Thickness = 0;
                t1.Border.Thickness = 2;
            }
        }

        void Handle_Test2(object sender, EventArgs e)
        {
            t2.Border.Thickness = 0;
            t1.Border.Thickness = 2;
        }
        void Handle_Test3(object sender, EventArgs e)
        {
            t1.Border.Thickness = 0;
            t2.Border.Thickness = 2;
        }

        void Handle_SaveFilterView(object sendr, EventArgs e)
        {
            currentNormalSetBlock = App.Database.GetNormalBlockWithChildren(currentNormalSetBlock.Id);
            currentNormalSetBlock.TimerOn = false;
            model.Timer timer = new model.Timer(currentNormalSetBlock.Hours, currentNormalSetBlock.Minutes, currentNormalSetBlock.Seconds);
            timer.UpdateDisplay();
            currentNormalSetBlock.TimerDisplay = timer.Display;
            currentNormalSetBlock.Width = 0;
            currentNormalSetBlock.XMargin = 40;
            App.Database.UpdateNormalBlockWithChildren(currentNormalSetBlock);
            FilterView.IsVisible = false;
            BindableLayout.SetItemsSource(BlockCollection, null);
            BindableLayout.SetItemsSource(BlockCollection, currentLogDay.GetAllBlocks());
        }

        void Handle_DiscardFilterView(object sendr, EventArgs e)
        {
            currentNormalSetBlock.Minutes = currentMin;
            currentNormalSetBlock.Seconds = currentSec;
            currentNormalSetBlock.MinutesSet = currentMin;
            currentNormalSetBlock.SecondsSet = currentSec;
            App.Database.UpdateNormalBlockWithChildren(currentNormalSetBlock);
            FilterView.IsVisible = false;
        }

        void Handle_AddPresetTime(object sender, EventArgs e)
        {
            Settings setting = App.Database.GetSettings();
            currentNormalSetBlock = App.Database.GetNormalBlockWithChildren(currentNormalSetBlock.Id);
            model.Timer timer = new model.Timer(currentNormalSetBlock.Hours, currentNormalSetBlock.Minutes, currentNormalSetBlock.Seconds);
            if(setting.PresetTimers.Count < 6)
            {
                timer.PresetMenuVisible = false;
                timer.IsPreset = true;
                timer.PresetOrder = 0;
                timer.UpdateDisplayWithoutHours();
                App.Database.SaveTimer(timer);
                setting.PresetTimers.Add(timer);
                App.Database.UpdateSettings(setting);

                BindableLayout.SetItemsSource(PresetsView, App.Database.GetSettings().PresetTimers);
            }
        }

        void Handle_DeletePresetTime(object sender, EventArgs e)
        {
            model.Timer timer = App.Database.GetTimer(((ImageButton)sender).CommandParameter);
            Settings settings = App.Database.GetSettings();
            App.Database.DeleteTimer(timer.Id);
            App.Database.UpdateSettings(settings);
            BindableLayout.SetItemsSource(PresetsView, App.Database.GetSettings().PresetTimers);

        }

        void Handle_UsePresetTime(object sender, EventArgs e)
        {
            model.Timer timer = App.Database.GetTimer(((Button)sender).CommandParameter);
            WheelPickerMinutes.SelectedItemsIndex = new List<int>() { timer.Minutes };
            WheelPickerSeconds.SelectedItemsIndex = new List<int>() { timer.Seconds };
            currentNormalSetBlock.Seconds = timer.Seconds;
            currentNormalSetBlock.SecondsSet = timer.Seconds;
            currentNormalSetBlock.Minutes = timer.Minutes;
            currentNormalSetBlock.MinutesSet = timer.Minutes;
            currentNormalSetBlock.TimerOn = false;
            App.Database.UpdateNormalBlockWithChildren(currentNormalSetBlock);
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
            try
            {
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
            }
            catch { }

            try
            {
                Blocks = currentLogDay.GetAllBlocks();
                BindableLayout.SetItemsSource(BlockCollection, Blocks);
            }
            catch { }
           
            //BindableLayout.SetItemsSource(ExerciseCollection, await GetAllExercises());
        }





        }
    }