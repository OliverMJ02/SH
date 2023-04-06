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


            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;

            //Button_StartWorkout.WidthRequest = xamarinWidth;
        }

        void Handle_ToExercises(object sender, EventArgs e)
        {
            ExerciseTab.IsVisible = true;
            DetailsTab.IsVisible = false;
        }
        void Handle_ToDetails(object sender, EventArgs e)
        {
            ExerciseTab.IsVisible = false;
            DetailsTab.IsVisible = true;
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }
        private async void Handle_ToOwnerView(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpenWorkoutOwner());
        }
    }
}