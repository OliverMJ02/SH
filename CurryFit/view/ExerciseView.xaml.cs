using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExerciseView : ContentPage
	{
		public ExerciseView ()
		{
			InitializeComponent ();
		}

        private async void Handle_ToExercises(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ExerciseView());
        }
        async void Handle_ToWorkouts(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutView());
        }
        private async void Handle_ToPrograms(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ProgramView());
        }
        private async void Handle_ToLogbook(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new LogbookView());
        }
        private async void Handle_OpenExercise(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OpenExercise());
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }
    }
}