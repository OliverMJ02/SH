using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Firebase.Database;
using Firebase.Database.Query;

using CurryFit.model.api;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManualAddPage : ContentPage
    {
        private FirebaseClient firebaseClient = new FirebaseClient("https://strengthhub-app-default-rtdb.europe-west1.firebasedatabase.app/");
        FoodProduct foodProduct;
        public ManualAddPage(string gtin)
        {
            InitializeComponent();
            foodProduct = new FoodProduct();
            foodProduct.gtin = gtin;
        }

        //Create a new food product make the user fill in all the values manually and then add it to the database

        private async void Handle_BackButton(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void AddProductButton_Clicked(object sender, EventArgs e)
        {
            
            //Check if all the fields are filled in
            if (ProductName.Text == null || Energy.Text == null || Carbs.Text == null || Fat.Text == null || Protein.Text == null)
            {
                await DisplayAlert("Error", "Please fill in all the fields", "OK");
            }
            else
            {
                //Create a new food product
                FoodProduct.Content contents = new FoodProduct.Content();
                foodProduct.Name = ProductName.Text;
                foodProduct.Brand = Brand.Text;
                
                contents.Size = Convert.ToDouble(Amount.Text);
                switch(Unit.SelectedItem)
                {
                    case "GRAM":
                        contents.Unit = "g";
                        break;
                    case "KILLOGRAM":
                        contents.Unit = "kg";
                        break;
                    case "DECILITER":
                        contents.Unit = "dl";
                        break;
                    case "LITER":
                        contents.Unit = "l";
                        break;
                }
                foodProduct.Contents = contents;
                foodProduct.Nutrients = new List<FoodProduct.Nutrient>();
                foodProduct.Nutrients.Add(new FoodProduct.Nutrient { Name = "Energi", Amount = Convert.ToDouble(Energy.Text), DailyIntake=0, Unit = "kcal" });
                foodProduct.Nutrients.Add(new FoodProduct.Nutrient { Name = "Kolhydrat", Amount = Convert.ToDouble(Carbs.Text), DailyIntake = 0, Unit = "g" });
                foodProduct.Nutrients.Add(new FoodProduct.Nutrient { Name = "Fett", Amount = Convert.ToDouble(Fat.Text), DailyIntake = 0, Unit = "g" });
                foodProduct.Nutrients.Add(new FoodProduct.Nutrient { Name = "Protein", Amount = Convert.ToDouble(Protein.Text), DailyIntake = 0, Unit = "g" });

                //Add the product to the database 
                try
                {
                    await firebaseClient.Child("FoodProducts").Child(foodProduct.Name).PutAsync(foodProduct);
                    await DisplayAlert("Success", "Product added", "OK");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.ToString(), "OK");
                }
                
                int BackCount = 2;
                for (var counter = 1; counter < BackCount; counter++)
                {
                    Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                }
                await Navigation.PopAsync();
            }
            
            
        }

    }
}