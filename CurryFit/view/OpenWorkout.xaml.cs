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
    public partial class OpenWorkout : ContentPage
    {
        public OpenWorkout()
        {
            InitializeComponent();
        }

        void Handle_ToExercises(object sender, EventArgs e)
        {
            ExerciseTab.IsVisible = true;
            DetailsTab.IsVisible = false;
            //ProgramLayout.IsVisible = false;
            //LogbookLayout.IsVisible = false;
        }
        void Handle_ToDetails(object sender, EventArgs e)
        {
            ExerciseTab.IsVisible = false;
            DetailsTab.IsVisible = true;
            //ProgramLayout.IsVisible = false;
            //LogbookLayout.IsVisible = false;
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }
    }
}