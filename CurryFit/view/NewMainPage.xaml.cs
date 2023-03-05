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
            CalenderF1.WidthRequest = xamarinWidth * 0.85;
            CalenderF2.WidthRequest = xamarinWidth * 0.85;
            DailyNutritionF1.WidthRequest = xamarinWidth * 0.85;
            DailyNutritionF2.WidthRequest = xamarinWidth * 0.85;
            ExerciseF1.WidthRequest = xamarinWidth * 0.85;
            ExerciseF2.WidthRequest = xamarinWidth * 0.85;
            ShopF1.WidthRequest = xamarinWidth * 0.85;
            ShopF2.WidthRequest = xamarinWidth * 0.85;
            ProgressF1.WidthRequest = xamarinWidth * 0.85;
            ProgressF2.WidthRequest = xamarinWidth * 0.85;

        }
        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

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