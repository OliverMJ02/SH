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
    public partial class OpenWorkoutOwner : ContentPage
    {
        List<(int, string)> list = new List<(int, string)>() { (0, "10"), (1,"20"), (2, "30") };
        public OpenWorkoutOwner()
        {
            InitializeComponent();


            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;
           
            
        
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

        void Handle_SliderVal(object sender, EventArgs e)
        {
            int value = (int)Math.Round(Slider_one.Value);
            Val_one.Text = value.ToString();

        }

        void Handle_SliderValTwo(object sender, EventArgs e)
        {
            int value = (int)Math.Round(Slider_two.Value);

            Val_two.Text = list[value].Item2;

        }

    }
}
