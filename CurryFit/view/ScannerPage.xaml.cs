using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Net.Mobile.Forms;

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
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scannat", result.Text, "OK");
            });
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