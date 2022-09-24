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

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        List<Exercise> ExercisesList = new List<Exercise>();
        ProgramExercise pex = new ProgramExercise
        {
            TpCreator = "All",
            TpDifficulty = "All",
            TpLocation = "All",
            TpMuscleGroups = "All",
            ExCreator = "All",
            ExDifficulty = "All",
            ExLocation = "All",
            ExMuscleGroup = "All",
            isProgram = true
        };
        public WorkoutPage()
        {
            InitializeComponent();

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;


            GridRowZero.HeightRequest = Math.Round(xamarinHeight) - 125;

            MenuBtn1.WidthRequest = Math.Round(xamarinWidth) / 5.5;
            MenuBtn2.WidthRequest = Math.Round(xamarinWidth) / 5.5;
            MenuBtn3.WidthRequest = Math.Round(xamarinWidth) / 5.5;
            MenuBtn4.WidthRequest = Math.Round(xamarinWidth) / 5.5;
            MenuBtn5.WidthRequest = Math.Round(xamarinWidth) / 5.5;

            CurrentExcerciseVideoLink.WidthRequest = Math.Round(xamarinWidth * 0.9);
            CurrentExcerciseVideoLink.HeightRequest = Math.Round(xamarinWidth * 0.5063);

            EditExcerciseDescription.HeightRequest = Math.Round(xamarinHeight * 0.15);
            EditExcerciseDescription.WidthRequest = Math.Round(xamarinWidth * 0.95);
            EditProgramDescription.HeightRequest = Math.Round(xamarinHeight * 0.15);
            EditProgramDescription.WidthRequest = Math.Round(xamarinWidth * 0.95);

            CreateExcerciseDescription.HeightRequest = Math.Round(xamarinHeight * 0.15);
            CreateExcerciseDescription.WidthRequest = Math.Round(xamarinWidth * 0.95);
            CreateProgramDescription.HeightRequest = Math.Round(xamarinHeight * 0.15);
            CreateProgramDescription.WidthRequest = Math.Round(xamarinWidth * 0.95);

            // Till för att fylla pickers med alternativ, går säkert att effektivisera eller fixa till bättre senare.
            CreateProgramDifficulty.Items.Add("Beginner");
            CreateProgramDifficulty.Items.Add("Easy");
            CreateProgramDifficulty.Items.Add("Normal");
            CreateProgramDifficulty.Items.Add("Hard");
            CreateProgramDifficulty.Items.Add("Expert");

            CreateExcerciseDifficulty.Items.Add("Beginner");
            CreateExcerciseDifficulty.Items.Add("Easy");
            CreateExcerciseDifficulty.Items.Add("Normal");
            CreateExcerciseDifficulty.Items.Add("Hard");
            CreateExcerciseDifficulty.Items.Add("Expert");

            EditExcerciseDifficulty.Items.Add("Beginner");
            EditExcerciseDifficulty.Items.Add("Easy");
            EditExcerciseDifficulty.Items.Add("Normal");
            EditExcerciseDifficulty.Items.Add("Hard");
            EditExcerciseDifficulty.Items.Add("Expert");

            EditProgramDifficulty.Items.Add("Beginner");
            EditProgramDifficulty.Items.Add("Easy");
            EditProgramDifficulty.Items.Add("Normal");
            EditProgramDifficulty.Items.Add("Hard");
            EditProgramDifficulty.Items.Add("Expert");

            CreateExcerciseLocation.Items.Add("Home");
            CreateExcerciseLocation.Items.Add("Gym");
            CreateExcerciseLocation.Items.Add("Outside");
            CreateExcerciseLocation.Items.Add("Sports center");
            CreateExcerciseLocation.Items.Add("Lake/Pool");

            CreateProgramLocation.Items.Add("Home");
            CreateProgramLocation.Items.Add("Gym");
            CreateProgramLocation.Items.Add("Outside");
            CreateProgramLocation.Items.Add("Sports center");
            CreateProgramLocation.Items.Add("Lake/Pool");

            EditExcerciseLocation.Items.Add("Home");
            EditExcerciseLocation.Items.Add("Gym");
            EditExcerciseLocation.Items.Add("Outside");
            EditExcerciseLocation.Items.Add("Sports center");
            EditExcerciseLocation.Items.Add("Lake/Pool");

            EditProgramLocation.Items.Add("Home");
            EditProgramLocation.Items.Add("Gym");
            EditProgramLocation.Items.Add("Outside");
            EditProgramLocation.Items.Add("Sports center");
            EditProgramLocation.Items.Add("Lake/Pool");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                trainingProgramsView.ItemsSource = App.Database.GetTrainingProgramsAsync();
            }
            catch { }

            try
            {
                exercisesView.ItemsSource = App.Database.GetExcercisesAsync();
            }
            catch { }
        }

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

                App.Database.SaveExcercisesAsync(excercise);
                exercisesView.ItemsSource = App.Database.GetExcercisesAsync();

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

                App.Database.SaveTrainingProgramAsync(trainingProgram);

                try
                {
                    trainingProgram.Excercises = ExercisesList;
                    App.Database.UpdateTrainingProgramWithChildren(trainingProgram);
                }
                catch { }

                ExercisesList.Clear();
                trainingProgramsView.ItemsSource = App.Database.GetTrainingProgramsAsync();
                collectionCurrentProgram.ItemsSource = App.Database.GetExcercisesAsync();

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
            collectionviewAddExcercise.ItemsSource = App.Database.GetExcercisesAsync();

            await CreateProgramScrollView.ScrollToAsync(0, 0, false);
        }


        void Handle_AddExcerciseToProgram(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;
            if (btn.Text.Equals("Remove Exercise"))
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
                btn.Text = "Add Exercise";

            }
            else
            {
                ExercisesList.Add(App.Database.GetSingleExcerciseAsync(id));
                btn.TextColor = Color.Red;
                btn.Text = "Remove Exercise";
            }

        }


        void Handle_ShowCurrentProgram(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;
            var item = (TapGestureRecognizer)stack.GestureRecognizers[0];
            object id = item.CommandParameter;

            BackToExcercises.CommandParameter = id;

            TrainingProgram trainingProgram = App.Database.GetSingleTrainingProgramAsync(id);
            CurrentProgramName.Text = trainingProgram.Name;
            CurrentProgramDescription.Text = trainingProgram.Description;
            CurrentProgramDifficulty.Text = trainingProgram.Difficulty;
            CurrentProgramMuscleGroups.Text = trainingProgram.MuscleGroups;
            CurrentProgramLocation.Text = trainingProgram.Location;
            CurrentExcerciseCreator.Text = trainingProgram.Creator;

            collectionCurrentProgram.ItemsSource = trainingProgram.Excercises;

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

            Exercise exercise = App.Database.GetSingleExcerciseAsync(id);
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

                App.Database.DeleteSingleExcerciseAsync(id);
                exercisesView.ItemsSource = App.Database.GetExcercisesAsync();

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

                App.Database.DeleteSingleTrainingProgramAsync(id);
                trainingProgramsView.ItemsSource = App.Database.GetTrainingProgramsAsync();

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

            Exercise excercise = App.Database.GetSingleExcerciseAsync(id);

            EditExcercise.IsVisible = true;
            CurrentExcercise.IsVisible = false;

            EditExcerciseDifficulty.Title = "Difficulty";

            EditExcerciseDescription.Text = excercise.Description;
            EditExcerciseDifficulty.SelectedItem = excercise.Difficulty;
            EditExcerciseLocation.SelectedItem = excercise.Location;
            EditExcerciseMuscleGroup.Text = excercise.MuscleGroup;
            EditExcerciseName.Text = excercise.Name;
            EditExcerciseVideoLink.Text = excercise.VideoLink;

            EditExcerciseSave.CommandParameter = id;

        }

        void Handle_SaveEditedExcercise(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;

            EditExcercise.IsVisible = false;
            CurrentExcercise.IsVisible = true;

            Exercise excercise = App.Database.GetSingleExcerciseAsync(id);
            excercise.Description = EditExcerciseDescription.Text;
            excercise.Difficulty = EditExcerciseDifficulty.SelectedItem.ToString();
            excercise.MuscleGroup = EditExcerciseMuscleGroup.Text;
            excercise.Location = EditExcerciseLocation.SelectedItem.ToString();
            excercise.Name = EditExcerciseName.Text;
            excercise.VideoLink = EditExcerciseVideoLink.Text;

            CurrentExcerciseName.Text = excercise.Name;
            CurrentExcerciseDescription.Text = excercise.Description;
            CurrentExcerciseDifficulty.Text = excercise.Difficulty;
            CurrentExcerciseMuscleGroup.Text = excercise.MuscleGroup;
            CurrentExcerciseVideoLink.Source = excercise.VideoLink;
            CurrentExcerciseCreator.Text = excercise.Creator;
            CurrentExcerciseLocation.Text = excercise.Location;

            App.Database.UpdateExcerciseAsync(excercise);
            exercisesView.ItemsSource = App.Database.GetExcercisesAsync();
        }

        async void Handle_EditProgram(object sender, EventArgs e)
        {
            await EditProgramScrollView.ScrollToAsync(0, 0, false);

            var btn = sender as Button;
            object id = btn.CommandParameter;

            TrainingProgram trainingProgram = App.Database.GetSingleTrainingProgramAsync(id);

            EditProgramDifficulty.Title = "Difficulty";

            EditProgramDescription.Text = trainingProgram.Description;
            EditProgramDifficulty.SelectedItem = trainingProgram.Difficulty;
            EditProgramLocation.SelectedItem = trainingProgram.Location;
            EditProgramMuscleGroups.Text = trainingProgram.MuscleGroups;
            EditProgramName.Text = trainingProgram.Name;

            EditProgram.IsVisible = true;
            CurrentProgram.IsVisible = false;
            CurrentProgramDeleteButton.IsVisible = false;

            foreach (Exercise exercise in App.Database.GetExcercisesAsync())
            {
                foreach (Exercise exercise2 in trainingProgram.Excercises)
                {
                    if (exercise2.Id == exercise.Id)
                    {
                        exercise.btnAddOrRemove = "Remove Exercise";
                        exercise.btnColor = "Red";
                        App.Database.UpdateExcerciseAsync(exercise);
                        break;
                    }
                    else
                    {
                        exercise.btnAddOrRemove = "Add Excercise";
                        exercise.btnColor = "DeepSkyBlue";
                        App.Database.UpdateExcerciseAsync(exercise);
                    }
                }

            }

            collectionviewEditExcercise.ItemsSource = App.Database.GetExcercisesAsync();

            SaveProgramBtnEdit.CommandParameter = id;
        }

        void Handle_SaveEditedProgram(object sender, EventArgs e)
        {
            var btn = sender as Button;
            object id = btn.CommandParameter;

            EditProgram.IsVisible = false;
            CurrentProgram.IsVisible = true;
            CurrentProgramDeleteButton.IsVisible = true;


            TrainingProgram program = App.Database.GetSingleTrainingProgramAsync(id);
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

            App.Database.UpdateTrainingProgramAsync(program);
            trainingProgramsView.ItemsSource = App.Database.GetTrainingProgramsAsync();
            collectionCurrentProgram.ItemsSource = program.Excercises;
        }

        void Handle_EditExcerciseToProgram(object sender, EventArgs e)
        {
            object idProgram = SaveProgramBtnEdit.CommandParameter;

            var btn = sender as Button;
            object id = btn.CommandParameter;
            Exercise excercise = App.Database.GetSingleExcerciseAsync(id);
            TrainingProgram program = App.Database.GetSingleTrainingProgramAsync(idProgram);

            if (btn.Text.Equals("Remove Excercise"))
            {
                List<Exercise> list = new List<Exercise>();
                foreach (Exercise ex in program.Excercises)
                {
                    if (ex.Id != excercise.Id)
                    {
                        list.Add(ex);
                    }
                }
                program.Excercises = list;
                App.Database.UpdateTrainingProgramWithChildren(program);
                btn.TextColor = Color.DeepSkyBlue;
                btn.Text = "Add Excercise";

            }
            else
            {
                program.Excercises.Add(excercise);
                App.Database.UpdateTrainingProgramWithChildren(program);
                btn.TextColor = Color.Red;
                btn.Text = "Remove Excercise";
            }
        }

        void Handle_UpdateProgramStar(object sender, EventArgs e)
        {
            var ImgBtn = sender as ImageButton;
            object id = ImgBtn.CommandParameter;

            try
            {
                TrainingProgram program = App.Database.GetSingleTrainingProgramAsync(id);

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
            List<TrainingProgram> tpstemp = App.Database.GetTrainingProgramsAsync();
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
                Exercise excercise = App.Database.GetSingleExcerciseAsync(id);

                if (excercise.isFavorised)
                {
                    excercise.isFavorised = false;
                    excercise.FavorisedSource = "unfilledStar.png";
                    ImgBtn.Source = "unfilledStar.png";
                    App.Database.UpdateExcerciseAsync(excercise);
                }
                else
                {
                    excercise.isFavorised = true;
                    excercise.FavorisedSource = "filledStar.png";
                    ImgBtn.Source = "filledStar.png";
                    App.Database.UpdateExcerciseAsync(excercise);
                }
            }

            catch { }

            //Behövs för att Excercise view ska bli updaterad med korrekta ex även vid filtrering, annars så blir det så att om man trycker på "stjärnan" och sedan direkt väljer att filtrera favoriserade så blir det fel då
            //Då filtering med favoriserade hänvisar till de som lär i itemsourcen redan och de är inte updaterade utan denna kod. Det blir lite omständigt eftersom man vill ju att "favorit filtreringen"
            //Också ska ta hänsyn till tidigare filtering och det går därför inte att bara kopiera in en alla excercises till "ItemSouce" för då tar den med excercises som kanske inte var med
            //i den tidigare filteringen. Väldigt rörigt men det funkar nu, Kanske går att lösa snyggare sen.
            List<Exercise> exstemp = App.Database.GetExcercisesAsync();
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
                List<TrainingProgram> tpsTemp = App.Database.GetTrainingProgramsAsync();
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
                List<Exercise> exsTemp = App.Database.GetExcercisesAsync();
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
        */
        }
    }
}