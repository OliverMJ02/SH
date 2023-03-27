using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewMainPage : ContentPage
    {
        public NewMainPage()
        {
            InitializeComponent();
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;

            // Calender frames
            CalenderF1.WidthRequest = xamarinWidth * 0.85;
            CalenderF2.WidthRequest = xamarinWidth * 0.85;

            //Nurition frames
            DailyNutritionF1.WidthRequest = xamarinWidth * 0.85;
            DailyNutritionF2.WidthRequest = xamarinWidth * 0.85;

            //Exercise frames
            ExerciseF1.WidthRequest = xamarinWidth * 0.85;
            ExerciseF2.WidthRequest = xamarinWidth * 0.85;

            //Shop frames
            ShopF1.WidthRequest = xamarinWidth * 0.85;
            ShopF2.WidthRequest = xamarinWidth * 0.85;

            //Progress / stat frames
            ProgressF1.WidthRequest = xamarinWidth * 0.85;
            ProgressF2.WidthRequest = xamarinWidth * 0.85;

            //Other frames
            //XPBar.WidthRequest = xamarinWidth * 0.15;
            KcalBarOutline.WidthRequest = xamarinWidth;
            CarbBarOutline.WidthRequest = xamarinWidth * 0.34;
            ProteinBarOutline.WidthRequest = xamarinWidth * 0.34;
            FatBarOutline.WidthRequest = xamarinWidth * 0.34;
            XPBarOutline.WidthRequest = xamarinWidth * 0.18;

            // KcalBar progress
            double ratio = 664.0 / 1926.0; // Ratio of (consumed Kcals / daily goal)
            double fullBar = 0.85 * xamarinWidth - 20; // Full bar width for the kcal progress bar
            KcalBarColored.TranslationX = -1*(fullBar - fullBar*(ratio));    // Used for calculating kcal progress

            Fade2.Color = Color.FromRgb(255, (int)(Math.Round((16*14-72)*(1-ratio)+72)), 16);

            //MacroNutrients
            //Carbs
            double CarbRatio = 66.0 / 120.0;
            CarbBarColored.TranslationX = -1 * (xamarinWidth - xamarinWidth * (CarbRatio));
            //Protein
            double ProteinRatio = 43.0 / 98.0;
            ProteinBarColored.TranslationX = -1 * (xamarinWidth - xamarinWidth * (ProteinRatio));
            //Fat
            double FatRatio = 21 / 64;
            FatBarColored.TranslationX= -1 * (xamarinWidth - xamarinWidth* (FatRatio));

            //XPBar
            double XPRatio = 45.0 / 100.0;
            XPBarColored.TranslationX = -1 * (xamarinWidth - xamarinWidth * (XPRatio));
            


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

        //Gidview shortcuts
        private void Handle_StepCounter(object sender, EventArgs e)
        {

        }

        private void Handle_UserShortcutPage(object sender, EventArgs e)
        {

        }

        private void Handle_Cardiocenter(object sender, EventArgs e)
        {

        }

        private void Handle_AddWaterPage(object sender, EventArgs e)
        {

        }

        
    }
}