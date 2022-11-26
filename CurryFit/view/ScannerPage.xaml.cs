using System;
using System.Threading.Tasks;
using ZXing;
using ZXing.Net.Mobile.Forms;
using CurryFit.model.api;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ZXingScannerPage
    { 

        public ScannerPage()
        {
            InitializeComponent();
        }

        private async void ZXingScannerPage_OnScanResult(ZXing.Result result)
        {
            string r = "0" + result.Text;
            FoodProduct product = await ApiHandler.GetProduct(r);
            await Navigation.PushAsync(new ScannedBarcodePage(product));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            IsScanning = false;
        }
    }
}