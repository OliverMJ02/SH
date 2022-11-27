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
        
        private void ZXingScannerPage_OnScanResult(ZXing.Result result)
        {
            FoodProduct product = ApiHandler.GetProductAndWaitOnResult(result.Text);
            if (product != null)
            {
                Device.BeginInvokeOnMainThread(async() =>
                {
                    await Navigation.PushAsync(new ScannedBarcodePage(product));
                });
                       
            }
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