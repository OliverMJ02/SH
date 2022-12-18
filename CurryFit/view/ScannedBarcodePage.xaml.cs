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
            EnergyLabel.Text = foodProduct.Nutrients[1].Amount.ToString() + " " + foodProduct.Nutrients[1].Unit;
            FatLabel.Text = foodProduct.Nutrients[2].Amount.ToString() + " " + foodProduct.Nutrients[2].Unit;
            CarbsLabel.Text = foodProduct.Nutrients[4].Amount.ToString() + " " + foodProduct.Nutrients[4].Unit;
            ProteinLabel.Text = foodProduct.Nutrients[6].Amount.ToString() + " " + foodProduct.Nutrients[6].Unit;

            populateProductChart();

        }

        private void populateProductChart()
        {
            var entries = new[]
       {
            new ChartEntry((float)foodProduct.Nutrients[2].Amount)
            {
                Color = SKColor.Parse("#FFE000")

            },
            new ChartEntry((float)foodProduct.Nutrients[4].Amount)
            {
                Color = SKColor.Parse("#2C91FF")
            },
             new ChartEntry((float)foodProduct.Nutrients[6].Amount)
            {
                Color = SKColor.Parse("#FF2656")
            }
        };


            ProductChart.Chart = new DonutChart { Entries = entries, HoleRadius = 0.8f, IsAnimated = true, BackgroundColor = SKColors.Transparent };
        }

        private void updateProductChart()
        {

        }

    }
}