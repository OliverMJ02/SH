using System;
using System.Threading.Tasks;

using Microcharts;
using SkiaSharp;
using CurryFit.model.api;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedBarcodePage : ContentPage
    {
        private FoodProduct foodProduct;
        private FirebaseClient firebaseClient = new FirebaseClient("https://strengthhub-app-default-rtdb.europe-west1.firebasedatabase.app/");
        public ScannedBarcodePage(FoodProduct product)
        {
            InitializeComponent();
            foodProduct = product;
            ProductName.Text = foodProduct.Name;
            CreatorName.Text = foodProduct.Brand + " | " + foodProduct.Contents.Size.ToString() + " " + foodProduct.Contents.Unit;
            EnergyLabel.Text = foodProduct.Nutrients[0].Amount.ToString() + " " + foodProduct.Nutrients[0].Unit;
            CarbsLabel.Text = foodProduct.Nutrients[1].Amount.ToString() + " " + foodProduct.Nutrients[1].Unit;
            FatLabel.Text = foodProduct.Nutrients[2].Amount.ToString() + " " + foodProduct.Nutrients[2].Unit;
            ProteinLabel.Text = foodProduct.Nutrients[3].Amount.ToString() + " " + foodProduct.Nutrients[3].Unit;
            SaltLabel.Text = foodProduct.Nutrients[4].Amount.ToString() + " " + foodProduct.Nutrients[4].Unit;
            DonutEnergyLabel.Text = EnergyLabel.Text;

            populateProductChart();
        }

        private void populateProductChart()
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
            ProductChart.Chart = new DonutChart { Entries = entries, HoleRadius = 0.8f, IsAnimated = true, BackgroundColor = SKColors.Transparent };
        }

        private void updateProductChart()
        {
            //update the labels with the new values
            EnergyLabel.Text = foodProduct.Nutrients[0].Amount.ToString() + " " + foodProduct.Nutrients[0].Unit;
            CarbsLabel.Text = foodProduct.Nutrients[1].Amount.ToString() + " " + foodProduct.Nutrients[1].Unit;
            FatLabel.Text = foodProduct.Nutrients[2].Amount.ToString() + " " + foodProduct.Nutrients[2].Unit;
            ProteinLabel.Text = foodProduct.Nutrients[3].Amount.ToString() + " " + foodProduct.Nutrients[3].Unit;
            SaltLabel.Text = foodProduct.Nutrients[4].Amount.ToString() + " " + foodProduct.Nutrients[4].Unit;

        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            //String can be empty
            if(string.IsNullOrEmpty(e.NewTextValue)) return;

            if(!double.TryParse(e.NewTextValue, out double value))
            {
                ((Entry) sender).Text = e.OldTextValue;
            }
            else
            {
                for (int i = 0; i < foodProduct.Nutrients.Count; i++)
                {
                    foodProduct.Nutrients[i].Amount = foodProduct.Nutrients[i].Amount * value;
                }
                updateProductChart();
             
            }
        }
        private async void AddProductClick(object sender, EventArgs e)
        {
            AddProductToDB();
            int BackCount = 2;
            for  (var counter = 1; counter < BackCount; counter++)
            {
                Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            }
            await Navigation.PopAsync();
        }

        private async void AddProductToDB()
        {
            // Add foodProduct to database if it doesn't exist and have the foodProduct.name as the key, if it does exist display error message
            var foodProductFromDB = await firebaseClient.Child("FoodProducts").Child(foodProduct.Name).OnceSingleAsync<FoodProduct>();
            if (foodProductFromDB == null)
            {
                await firebaseClient.Child("FoodProducts").Child(foodProduct.Name).PutAsync(foodProduct);
            }
            else
            {
                await DisplayAlert("Error", "Product already exists in database", "OK");
            }
      
        }
    }
}