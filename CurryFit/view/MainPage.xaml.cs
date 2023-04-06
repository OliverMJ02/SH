using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using CurryFit.view;
using Firebase.Database;
using CurryFit.model;
using CurryFit.model.user;

namespace CurryFit
{
    public partial class MainPage : ContentPage
    {
        FirebaseClient firebaseClient = new Firebase.Database.FirebaseClient("https://projectspice-shoof-default-rtdb.europe-west1.firebasedatabase.app/");
        List<Exercise> LocalExercises = new List<Exercise>();
        List<Exercise> CloudExercises = new List<Exercise>();


        public MainPage()
        {
            InitializeComponent();
        }
        private async void Handle_NewMainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }
        private async void Handle_WorkoutPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutPage());
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void Handle_FoodPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FoodPage());
        }

        private async void Handle_WorkoutView(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutView());
        }

        private async void Handle_LoginPage(object sender, EventArgs e)
        {
            var authService = DependencyService.Get<IAuthHandler>();
            await Navigation.PushAsync(new LoginPage(authService));
        }

        public async Task<List<Exercise>> GetAllExercises()
        {

            return (await firebaseClient
              .Child("Exercises")
              .OnceAsync<Exercise>()).Select(item => new Exercise
              {
                  Id = item.Object.Id,
                  Key = item.Key,
                  Name = item.Object.Name,
                  Description = item.Object.Description,
                  Creator = item.Object.Creator,
                  MainMuscle = item.Object.MainMuscle,
                  MainEquipment = item.Object.MainEquipment,
                  FavorisedSource = item.Object.FavorisedSource,
              }).ToList();
        }

        public async Task<Exercise> GetSingleExercises(string key)
        {
            foreach (Exercise ex in (await GetAllExercises()))
            {
                if (ex.Key.Equals(key))
                {
                    return ex;
                }
            }
            return null;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                CloudExercises = await GetAllExercises();
                LocalExercises = App.Database.GetExercises();
            }
            catch { }
            foreach (Exercise ex in CloudExercises)
                {
                    if (!ex.CheckExistence(LocalExercises))
                    {
                        App.Database.SaveExercise(ex);
                    }
                }
            
        }



    }
}
