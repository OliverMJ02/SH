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
    public partial class WorkoutView : ContentPage
    {
        public WorkoutView()
        {
            InitializeComponent();
        }

        void Handle_ToExercises(object sender, EventArgs e)
        {
            //ExerciseLayout.IsVisible = true;
            WorkoutLayout.IsVisible = false;
            //ProgramLayout.IsVisible = false;
            //LogbookLayout.IsVisible = false;
        }
        void Handle_ToWorkouts(object sender, EventArgs e)
        {
            //ExerciseLayout.IsVisible = false;
            WorkoutLayout.IsVisible = true;
            //ProgramLayout.IsVisible = false;
            //LogbookLayout.IsVisible = false;
        }
        void Handle_ToPrograms(object sender, EventArgs e)
        {
            //ExerciseLayout.IsVisible = false;
            WorkoutLayout.IsVisible = false;
            //ProgramLayout.IsVisible = true;
            //LogbookLayout.IsVisible = false;
        }
        void Handle_ToLogbook(object sender, EventArgs e)
        {
            //ExerciseLayout.IsVisible = false;
            WorkoutLayout.IsVisible = false;
            //ProgramLayout.IsVisible = false;
            //LogbookLayout.IsVisible = true;
        }


        private async void Handle_OpenWorkout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpenWorkout());
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }
    }
}