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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScannerPage : ZXingScannerPage
    {
        static HttpClient client = new HttpClient();

        private readonly string api_key = "?apikey=8e8d6aa6-4112-4eae-a287-22bca4ab5207";

        private readonly string base_url = "https://api.dabas.com/DABAService/V2/article/gtin/"; 

        public ScannerPage()
        {
            InitializeComponent();
            client.BaseAddress = new Uri(base_url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void ZXingScannerPage_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scannat", result.Text, "OK");

                HttpRequestMessage request = new HttpRequestMessage();
                request.Method = HttpMethod.Get;


                client.Dispose();
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