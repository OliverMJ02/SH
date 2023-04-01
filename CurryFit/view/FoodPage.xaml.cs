using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CurryFit.model;
using SkiaSharp;
using Microcharts;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : TabbedPage
    {

        Calendar calendar = new Calendar();
        public FoodPage()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;

            dateLabel.Text = DateTime.Now.ToString("dd MMMM yyyy");
            SearchBar.WidthRequest = xamarinWidth - (15*4 + 34*2); // Width of searchbar, expands to make gap between searchbar and imagebuttons constant undependent on screenwidth
            KcalBarOutline.WidthRequest = xamarinWidth;

            // KcalBar progress
            double ratio = 1641.0 / 1761.0; // Ratio of (consumed Kcals / daily goal)
            double fullBar = 0.85 * xamarinWidth - 20; // Full bar width for the kcal progress bar
            KcalBarColored.TranslationX = -1 * (fullBar - fullBar * (ratio));    // Used for calculating kcal progress

            Fade2.Color = Color.FromRgb(255, (int)(Math.Round((16 * 14 - 72) * (1 - ratio) + 72)), 16);

            // WaterFrame
            WaterFrame.WidthRequest = xamarinWidth;
        }
        private async void Handle_ScannerPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScannerPage());
        }

        private async void Handle_ManualPage(object sender, EventArgs e)
        {
            //Should be changed
            await Navigation.PushAsync(new ManualAddPage("2"));
        }
        
        private void Previous_Date(object sender, EventArgs e)
        {
            dateLabel.Text = calendar.Get_PreviousDay(dateLabel.Text);
        }
        private void Next_Date(object sender, EventArgs e)
        {
            dateLabel.Text = calendar.Get_NextDay(dateLabel.Text);
        }

        //Navbar
        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        private async void Handle_FoodPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodPage());
        }

        private async void Handle_WorkoutPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutPage());
        }

        private async void Handle_StatPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatPage());
        }

        private async void Handle_ProfilePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilePage());
        }

        /*
        private void populateCarbChart()
        {
            var entries = new[]
       {
         new ChartEntry((float)foodProduct.Nutrients[1].Amount)
            {
                Color = SKColor.Parse("#2C91FF")
            },
            new ChartEntry((float)foodProduct.Nutrients[2].Amount)
            {
                Color = SKColor.Parse("#FFE000")

            },
             new ChartEntry((float)foodProduct.Nutrients[3].Amount)
            {
                Color = SKColor.Parse("#FF2656")
            }
        };
            CarbChart.Chart = new DonutChart { Entries = entries, HoleRadius = 0.8f, IsAnimated = true, BackgroundColor = SKColors.Transparent };
        }
        */

    }
}