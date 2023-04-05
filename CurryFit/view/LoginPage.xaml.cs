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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();



            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;

            boxone.HeightRequest = (xamarinHeight - 600) / 8;
            boxtwo.HeightRequest = (xamarinHeight - 600) / 8;
            boxthree.HeightRequest = (xamarinHeight - 600) / 4;
            //double bone = (xamarinHeight - 600) / 4;
            //fu.Text = xamarinHeight.ToString();
            //uf.Text = bone.ToString();
        }

        private async void Handle_ToCreateAccount(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccountPage());
        }
    }
}