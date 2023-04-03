using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Xamarin.CommunityToolkit.Extensions;
using CurryFit.model;
using CurryFit.model.blocks;
using Firebase.Database;
using Firebase.Database.Query;
using CurryFit.model.Sets;
using System.Threading;
using System.Globalization;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace CurryFit.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenWorkoutOwner : ContentPage
    {
        List<(int, string)> RestList = new List<(int, string)>() { (0, "0:10"), (1, "0:20"), (2, "0:30"), (3, "0:45"), (4, "1:00"), (5, "1:15"), (6, "1:30"), (7, "1:45"), (8, "2:00"), (9, "2:30"), (10, "3:00"), (11, "3:30"), (12, "4:00"), (13, "4:30"), (14, "5:00") };
        List<(int, string)> RepsList = new List<(int, string)>() { (0, "1-3"), (1, "1-5"), (2, "6-8"), (3, "8-12"), (4, "12-16"), (5, "16-20"), (6, "20-30"), (7, "30-40"), (8, "40-50"), (9, "50+") };

        

        bool VarRepBool;


        public OpenWorkoutOwner()
        {
            InitializeComponent();


            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var deviceHeight = mainDisplayInfo.Height;
            var deviceWidth = mainDisplayInfo.Width;
            var density = mainDisplayInfo.Density;
            var xamarinHeight = deviceHeight / mainDisplayInfo.Density;
            var xamarinWidth = deviceWidth / mainDisplayInfo.Density;

            

            RepToFailureToggle.Toggled += (sender, args) =>
            {

                if (args.Value == true)
                {
                    RepsBox.IsVisible = false;
                }

                if (args.Value == false)
                {
                    RepsBox.IsVisible = true;
                }
            };

            VarRepToggle.Toggled += (sender, args) =>
            {
                
                if (args.Value == true)
                {
                    VarRepBool = true;
                }

                if (args.Value == false)
                {
                    VarRepBool = false;
                }
            };
           



        }


        void Handle_ToExercises(object sender, EventArgs e)
        {
            ExerciseTab.IsVisible = true;
            DetailsTab.IsVisible = false;
        }
        void Handle_ToDetails(object sender, EventArgs e)
        {
            ExerciseTab.IsVisible = false;
            DetailsTab.IsVisible = true;
        }

        private async void Handle_MainPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewMainPage());
        }
  

        void Handle_SliderValNrSets(object sender, EventArgs e)
        {

            int i;
            int value = (int)Math.Round(Slider_NrSets.Value);
            Val_NrSets.Text = value.ToString();

            if (VarRepBool == true) {




                for (i = ParentSetInstance.Children.Count - 1; i >= 0; i--)
                {
                    ParentSetInstance.Children.RemoveAt(i);
                }

                // Add new copies
                for ( i = 0; i < value; i++)
                {
                    ParentSetInstance.Children.Add(new StackLayout
                    {
                        // Customize the properties of each StackLayout as desired
                        BackgroundColor = Color.LightGray,
                        HeightRequest = 50,
                        Margin = new Thickness(10, 5, 10, 5)


                    });
                }
            }

            else
            {
                
                for (i = ParentSetInstance.Children.Count -1; i >=0; i--)
                {
                    ParentSetInstance.Children.RemoveAt(i);
                }


            }



            //if (value > 1) {
            //    for (int i = 0; i < value; i++)
            //    {
            //        ParentSetInstance.Children.Add(new StackLayout
            //        {
            //            // Customize the properties of each StackLayout as desired
            //            BackgroundColor = Color.LightGray,
            //            HeightRequest = 50,
            //            Margin = new Thickness(10, 5, 10, 5)
            //        });
            //    }


            //    // Set the content of the page to be the stack layout
            //    //ParentSetInstance = SetInstance;

            //    SetInstance.Children.Clear();

            //}
            // Add some views to the stack layout
        }



        void Handle_SliderNrReps(object sender, EventArgs e)
        {
            int value = (int)Math.Round(Slider_NrReps.Value);

            Val_NrReps.Text = RepsList[value].Item2;

        }

        void Handle_SliderValRest(object sender, EventArgs e)
        {
            int value = (int)Math.Round(Slider_Rest.Value);

            Val_Rest.Text = RestList[value].Item2;

        }

    }
}
