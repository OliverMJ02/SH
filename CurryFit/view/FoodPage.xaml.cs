using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CurryFit.model;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        //Calendar calendar = new Calendar();
        public FoodPage()
        {
            InitializeComponent();
            dateLabel.Text = DateTime.Now.ToString("dd MM yyyy");
        }
        private async void Handle_ScannerPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScannerPage());
        }
        
        private void Previous_Date(object sender, EventArgs e)
        {
            //dateLabel.Text = calendar.get_PreviousDay(dateLabel.Text);
        }
        private void Next_Date(object sender, EventArgs e)
        {
            //dateLabel.Text = calendar.get_NextDay(dateLabel.Text);
        }
    }
}