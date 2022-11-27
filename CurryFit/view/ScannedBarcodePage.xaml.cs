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
        public ScannedBarcodePage(FoodProduct product)
        {
            InitializeComponent();
            FoodProduct foodProduct = product;
            ProductName.Text = foodProduct.Name;
            FatLabel.Text = foodProduct.Nutrients[2].Amount.ToString();
        }

        /*
        public Chart ProductChart => new DonutChart()
        {
            Entries = new[]
            {
                new ChartEntry((float)foodProduct.Nutrients[2].Amount)
                {
                    Color = SKColor.Parse("#FFE000")
                    
                },
                new ChartEntry((float)foodProduct.Nutrients[4].Amount)
                {
                    Color = SKColor.Parse("#2C91FF")
                }
            }
        };
        */
    }
}