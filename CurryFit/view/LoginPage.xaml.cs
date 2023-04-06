using CurryFit.model.user;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private readonly IAuthHandler authHandler;
        public LoginPage(IAuthHandler _authHandler)
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

        private async void Handle_ToCreateAccount(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateAccountPage(authHandler));
        }
        private async void Handle_Login(object sender, EventArgs e)
        {
            string email = emailEntry.Text.ToString();
            string password = passwordEntry.Text.ToString();
            await authHandler.Login(email, password);
        }
        
    }
}