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
    public partial class OpenExercise : ContentPage
    {
        public OpenExercise()
        {
            InitializeComponent();
        }
        void Handle_ToInstructions(object sender, EventArgs e)
        {
            InstructionsTab.IsVisible = true;
            InformationTab.IsVisible = false;
            TutorialsTab.IsVisible = false;
        }
        void Handle_ToInformation(object sender, EventArgs e)
        {
            InstructionsTab.IsVisible = false;
            InformationTab.IsVisible = true;
            TutorialsTab.IsVisible = false;
        }
        void Handle_ToTutorials(object sender, EventArgs e)
        {
            InstructionsTab.IsVisible = false;
            InformationTab.IsVisible = false;
            TutorialsTab.IsVisible = true;
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }

        private async void OnVideo1Tapped(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.youtube.com/watch?v=DLzxrzFCyOs"));
        }
        private async void OnVideo2Tapped(object sender, EventArgs e)
        {
            await Launcher.OpenAsync(new Uri("https://www.youtube.com/watch?v=xvFZjo5PgG0"));
        }

    }
}