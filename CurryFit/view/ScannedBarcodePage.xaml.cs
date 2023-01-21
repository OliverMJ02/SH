using System;
using System.Threading.Tasks;

using Microcharts;
using SkiaSharp;
using CurryFit.model.api;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannedBarcodePage : ContentPage
    {
        FoodProduct foodProduct;
        public ScannedBarcodePage(FoodProduct product)
        {
            InitializeComponent();
            foodProduct = product;
            ProductName.Text = foodProduct.Name;
            EnergyLabel.Text = foodProduct.Nutrients[0].Amount.ToString() + " " + foodProduct.Nutrients[0].Unit;
            CarbsLabel.Text = foodProduct.Nutrients[1].Amount.ToString() + " " + foodProduct.Nutrients[1].Unit;
            FatLabel.Text = foodProduct.Nutrients[2].Amount.ToString() + " " + foodProduct.Nutrients[2].Unit;
            ProteinLabel.Text = foodProduct.Nutrients[3].Amount.ToString() + " " + foodProduct.Nutrients[3].Unit;
            FiberLabel.Text = foodProduct.Nutrients[4].Amount.ToString() + " " + foodProduct.Nutrients[4].Unit;

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
            FiberLabel.Text = foodProduct.Nutrients[4].Amount.ToString() + " " + foodProduct.Nutrients[4].Unit;

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
        private void AddProductClick(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}