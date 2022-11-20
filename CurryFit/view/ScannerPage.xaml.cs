using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Net.Mobile.Forms;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            FoodProduct product = await ApiHandler.GetProduct(result.Text);
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