using CurryFit.model.user;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccountPage : ContentPage
    {
        private readonly IAuthHandler authHandler;

        public CreateAccountPage(IAuthHandler _authHandler)
        {
            InitializeComponent();
            authHandler = _authHandler;

            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;
        }

        private async void Handle_ToLogin(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void Handle_SignUp(object sender, EventArgs e)
        {
            string email = emailEntry.Text.ToString();
            string password = passwordEntry.Text.ToString();
            try
            {
                await authHandler.SignUp(email, password);
                await DisplayAlert("Success", "Account Created", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString() , "OK");
            }
        }
    }
}