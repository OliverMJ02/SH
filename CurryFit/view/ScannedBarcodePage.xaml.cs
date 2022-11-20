using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void populateLabels()
        {
            
            ProductName.Text = foodProduct.Name;
        }

        private async void Handle_ProductDetailView(object sender, EventArgs e)
        {
           // await Navigation.PushAsync()
        }
    }
}