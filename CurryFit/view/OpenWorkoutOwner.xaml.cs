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

        List<(int, string)> RestList2 = new List<(int, string)>() { (0, "0:10"), (1, "0:20"), (2, "0:30"), (3, "0:45"), (4, "1:00"), (5, "1:15"), (6, "1:30"), (7, "1:45"), (8, "2:00"), (9, "2:30"), (10, "3:00"), (11, "3:30"), (12, "4:00"), (13, "4:30"), (14, "5:00") };
        List<(int, string)> RepsList2 = new List<(int, string)>() { (0, "1-3"), (1, "1-5"), (2, "6-8"), (3, "8-12"), (4, "12-16"), (5, "16-20"), (6, "20-30"), (7, "30-40"), (8, "40-50"), (9, "50+") };

        int sliderValue;

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
                    
                        // Create and add new stack layouts based on the slider value
                        // Create and add new stack layouts based on the slider value
                        for (int i = 0; i < sliderValue-1; i++)
                        {
                        
                        int n = i + 2;
                        Val_NrSets.Text = n.ToString();
                        //SetInstance2
                        var stackLayout = new StackLayout
                            {
                                Children =
                    {
                        new BoxView{
                            BackgroundColor = (Color)App.Current.Resources["SPGrey"],
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HeightRequest = 1,
                            Margin = new Thickness(15, 15, 15, 0)
                            },
                        new StackLayout
                        {
                            Spacing = 0,
                            Orientation = StackOrientation.Horizontal,
                            Padding = new Thickness(15, 20, 0, 10),
                            Margin = new Thickness(0, 0, 0, 0),
                            Children =
                            {
                                new Label{
                                Text = "SET ",
                                FontSize = 14,
                                TextTransform = TextTransform.Uppercase,
                                FontFamily = "M",
                                TextColor = (Color)App.Current.Resources["RiceWhite"],
                                Margin = new Thickness(0, 0, 0, 0),
                                VerticalOptions = LayoutOptions.Center
                                },
                                new Label{
                                    Text = n.ToString(),
                                    FontSize = 14,
                                    TextTransform = TextTransform.Uppercase,
                                    FontFamily = "M",
                                    TextColor = (Color)App.Current.Resources["RiceWhite"],
                                    Margin = new Thickness(0, 0, 0, 0),
                                    VerticalOptions = LayoutOptions.Center}
                                    }
                        },
                        //Repetitions
                        new StackLayout
                        {
                            IsVisible = true,
                            Spacing = 0,
                            Orientation = StackOrientation.Vertical,
                            Padding = new Thickness(15, 10, 15, 10),
                            Margin = new Thickness(0, 0, 0, 0),
                            Children =
                            {
                                new StackLayout
                                {
                                Spacing = 0,
                                Orientation = StackOrientation.Horizontal,
                                Padding = new Thickness(0, 5, 0, 0),
                                Margin = new Thickness(0, -6, 0, 0),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Children =
                                {
                                new Label
                                {
                                    Text = "repetitions",
                                    TextTransform = TextTransform.Uppercase,
                                    FontSize = 14,
                                    FontFamily = "M",
                                    TextColor = (Color)App.Current.Resources["AshGrey"],
                                    HorizontalOptions = LayoutOptions.StartAndExpand,
                                    Margin = new Thickness(0, 0, 0, 0),
                                    Padding = new Thickness(0, 0, 0, 0)
                                },
                                new Label
                                {
                                    Text = "NO REPS",
                                    FontSize = 14,
                                    FontFamily = "M",
                                    TextColor = (Color)App.Current.Resources["AshGrey"],
                                    HorizontalOptions = LayoutOptions.End,
                                    Margin = new Thickness(0, 0, 0, 0),
                                    Padding = new Thickness(0, 0, 0, 0)
                                }}},
                                new StackLayout
                                {
                                    Spacing = 0,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Padding = new Thickness(0, 20, 0, 0),
                                    Margin = new Thickness(0, -6, 0, 0),
                                    Children =
                                    {
                                        new Slider
                                        {
                                            Maximum = 9,
                                            Minimum = 0,
                                            Scale = 2,
                                            MinimumTrackColor = Color.White,
                                            MaximumTrackColor = (Color)App.Current.Resources["AshGrey"],
                                            ThumbColor = (Color)App.Current.Resources["AshGrey"],
                                            Margin = new Thickness(64, 0, 64, 0)
                                        }
                                    }
                                }
                            }
                        },
                        //Rep to failure
                        new StackLayout
                        {
                        Spacing = 0,
                        Orientation = StackOrientation.Vertical,
                        Padding = new Thickness(15, 10, 15, 10),
                        Margin = new Thickness(0),
                        Children = {
                                new StackLayout
                                {
                                    Spacing = 0,
                                    Orientation = StackOrientation.Horizontal,
                                    Padding = new Thickness(0, 5, 0, 0),
                                    Margin = new Thickness(0, -6, 0, 0),
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Children = {
                                        new Label
                                        {
                                            Text = "rep to failure",
                                            TextTransform = TextTransform.Uppercase,
                                            FontSize = 14,
                                            FontFamily = "M",
                                            TextColor = (Color)Application.Current.Resources["AshGrey"],
                                            HorizontalOptions = LayoutOptions.StartAndExpand,
                                            Margin = new Thickness(0)
                                        },
                                        new Switch
                                        {
                                            ThumbColor = (Color)Application.Current.Resources["AshGrey"],
                                            OnColor = (Color)Application.Current.Resources["RiceWhite"],
                                            Scale = 1.5,
                                            Margin = new Thickness(0, -5, 5, 0)
                                        }
                                    }
                                }
                            }
                        },
                        //Rest timer
                        new StackLayout
                        {
                            Spacing = 0,
                            Orientation = StackOrientation.Vertical,
                            Padding = new Thickness(15, 10, 15, 10),
                            Margin = new Thickness(0, 0, 0, 0),
                            Children = {
                                new StackLayout
                                {
                                    Spacing = 0,
                                    Orientation = StackOrientation.Horizontal,
                                    Padding = new Thickness(0, 5, 0, 0),
                                    Margin = new Thickness(0, -6, 0, 0),
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Children = {
                                        new Label {
                                        Text = "rest time",
                                        TextTransform = TextTransform.Uppercase,
                                        FontSize = 14,
                                        FontFamily = "M",
                                        TextColor = (Color)App.Current.Resources["AshGrey"],
                                        HorizontalOptions = LayoutOptions.StartAndExpand,
                                        Margin = new Thickness(0, 0, 0, 0),
                                        Padding = new Thickness(0, 0, 0, 0)
                                        },
                                        new Label{
                                        Text = "NO TIME",
                                        FontSize = 14,
                                        FontFamily = "M",
                                        TextColor = (Color)App.Current.Resources["AshGrey"],
                                        HorizontalOptions = LayoutOptions.End,
                                        Margin = new Thickness(0, 0, 0, 0),
                                        Padding = new Thickness(0, 0, 0, 0),
                                        }
                                    }
                                },
                                new StackLayout
                                {
                                    Spacing = 0,
                                    HorizontalOptions = LayoutOptions.FillAndExpand,
                                    Padding = new Thickness(0, 20, 0, 0),
                                    Margin = new Thickness(0, -6, 0, 0),
                                    Children = {
                                        new Slider
                                        {
                                            Maximum = 14,
                                            Minimum = 0,
                                            Value = 0,
                                            Scale = 2,
                                            MinimumTrackColor = Color.White,
                                            MaximumTrackColor = (Color)App.Current.Resources["AshGrey"],
                                            ThumbColor = (Color)App.Current.Resources["AshGrey"],
                                            Margin = new Thickness(64, 0, 64, 0)
                                        }
                                    }
                                }
                            }

                        }
            }
                            };

                            TitleSetOne.IsVisible = true;
                            ParentSetInstance.Children.Add(stackLayout);
                        
                    }




                }

                if (args.Value == false)
                {
                    VarRepBool = false;
                    TitleSetOne.IsVisible = false;
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


        //    void Handle_SliderValNrSets(object sender, EventArgs e)
        //    {

        //        int i;
        //        int value = (int)Math.Round(Slider_NrSets.Value);
        //        Val_NrSets.Text = value.ToString();

        //        if (VarRepBool == true) {




        //            for (i = ParentSetInstance.Children.Count - 1; i >= 0; i--)
        //            {
        //                ParentSetInstance.Children.RemoveAt(i);
        //            }

        //            // Add new copies
        //            for (i = 0; i < value; i++)
        //            {
        //                var stackLayout = new StackLayout
        //                {






        //            };
        //            }
        //            SetInstance2.IsVisible = true;
        //            TitleSetOne.IsVisible = true;
        //        }





        //        else
        //        {

        //            for (i = ParentSetInstance.Children.Count -1; i >=0; i--)
        //            {

        //                SetInstance2.IsVisible = false;
        //                TitleSetOne.IsVisible = false;
        //            }
        //        }
        //}





        private void Handle_SliderValNrSets(object sender, ValueChangedEventArgs e)
        {
            // Clear any existing children in the parent layout maybe 
            ParentSetInstance.Children.Clear();

            // Get the new slider value
            sliderValue = (int)Math.Round(Slider_NrSets.Value);
            Val_NrSets.Text = sliderValue.ToString();

            //if (VarRepToggle.IsToggled == true)
            //{
            //    // Create and add new stack layouts based on the slider value
            //   // Create and add new stack layouts based on the slider value
            //for (int i = 0; i < sliderValue; i++)
            //{
            //        int n = i + 2;
            //    //SetInstance2
            //    var stackLayout = new StackLayout
            //    {
            //        Children =
            //        {
            //            new BoxView{
            //                BackgroundColor = (Color)App.Current.Resources["SPGrey"],
            //                HorizontalOptions = LayoutOptions.FillAndExpand,
            //                HeightRequest = 1,
            //                Margin = new Thickness(15, 15, 15, 0)
            //                },
            //            new StackLayout
            //            {
            //                Spacing = 0,
            //                Orientation = StackOrientation.Horizontal,
            //                Padding = new Thickness(15, 20, 0, 10),
            //                Margin = new Thickness(0, 0, 0, 0),
            //                Children =
            //                {
            //                    new Label{
            //                    Text = "SET ",
            //                    FontSize = 14,
            //                    TextTransform = TextTransform.Uppercase,
            //                    FontFamily = "M",
            //                    TextColor = (Color)App.Current.Resources["RiceWhite"],
            //                    Margin = new Thickness(0, 0, 0, 0),
            //                    VerticalOptions = LayoutOptions.Center
            //                    },
            //                    new Label{
            //                        Text = n.ToString(),
            //                        FontSize = 14,
            //                        TextTransform = TextTransform.Uppercase,
            //                        FontFamily = "M",
            //                        TextColor = (Color)App.Current.Resources["RiceWhite"],
            //                        Margin = new Thickness(0, 0, 0, 0),
            //                        VerticalOptions = LayoutOptions.Center}
            //                        }
            //            },
            //            //Repetitions
            //            new StackLayout
            //            {
            //                IsVisible = true,
            //                Spacing = 0,
            //                Orientation = StackOrientation.Vertical,
            //                Padding = new Thickness(15, 10, 15, 10),
            //                Margin = new Thickness(0, 0, 0, 0),
            //                Children =
            //                {
            //                    new StackLayout
            //                    {
            //                    Spacing = 0,
            //                    Orientation = StackOrientation.Horizontal,
            //                    Padding = new Thickness(0, 5, 0, 0),
            //                    Margin = new Thickness(0, -6, 0, 0),
            //                    HorizontalOptions = LayoutOptions.FillAndExpand,
            //                    Children =
            //                    {
            //                    new Label
            //                    {
            //                        Text = "repetitions",
            //                        TextTransform = TextTransform.Uppercase,
            //                        FontSize = 14,
            //                        FontFamily = "M",
            //                        TextColor = (Color)App.Current.Resources["AshGrey"],
            //                        HorizontalOptions = LayoutOptions.StartAndExpand,
            //                        Margin = new Thickness(0, 0, 0, 0),
            //                        Padding = new Thickness(0, 0, 0, 0)
            //                    },
            //                    new Label
            //                    {
            //                        Text = "NO REPS",
            //                        FontSize = 14,
            //                        FontFamily = "M",
            //                        TextColor = (Color)App.Current.Resources["AshGrey"],
            //                        HorizontalOptions = LayoutOptions.End,
            //                        Margin = new Thickness(0, 0, 0, 0),
            //                        Padding = new Thickness(0, 0, 0, 0)
            //                    }}},
            //                    new StackLayout
            //                    {
            //                        Spacing = 0,
            //                        HorizontalOptions = LayoutOptions.FillAndExpand,
            //                        Padding = new Thickness(0, 20, 0, 0),
            //                        Margin = new Thickness(0, -6, 0, 0),
            //                        Children =
            //                        {
            //                            new Slider
            //                            {
            //                                Maximum = 9,
            //                                Minimum = 0,
            //                                Scale = 2,
            //                                MinimumTrackColor = Color.White,
            //                                MaximumTrackColor = (Color)App.Current.Resources["AshGrey"],
            //                                ThumbColor = (Color)App.Current.Resources["AshGrey"],
            //                                Margin = new Thickness(64, 0, 64, 0)
            //                            }
            //                        }
            //                    }
            //                }
            //            },
            //            //Rep to failure
            //            new StackLayout
            //            {
            //            Spacing = 0,
            //            Orientation = StackOrientation.Vertical,
            //            Padding = new Thickness(15, 10, 15, 10),
            //            Margin = new Thickness(0),
            //            Children = {
            //                    new StackLayout
            //                    {
            //                        Spacing = 0,
            //                        Orientation = StackOrientation.Horizontal,
            //                        Padding = new Thickness(0, 5, 0, 0),
            //                        Margin = new Thickness(0, -6, 0, 0),
            //                        HorizontalOptions = LayoutOptions.FillAndExpand,
            //                        Children = {
            //                            new Label
            //                            {
            //                                Text = "rep to failure",
            //                                TextTransform = TextTransform.Uppercase,
            //                                FontSize = 14,
            //                                FontFamily = "M",
            //                                TextColor = (Color)Application.Current.Resources["AshGrey"],
            //                                HorizontalOptions = LayoutOptions.StartAndExpand,
            //                                Margin = new Thickness(0)
            //                            },
            //                            new Switch
            //                            {
            //                                ThumbColor = (Color)Application.Current.Resources["AshGrey"],
            //                                OnColor = (Color)Application.Current.Resources["RiceWhite"],
            //                                Scale = 1.5,
            //                                Margin = new Thickness(0, -5, 5, 0)
            //                            }
            //                        }
            //                    }
            //                }
            //            },
            //            //Rest timer
            //            new StackLayout
            //            {
            //                Spacing = 0,
            //                Orientation = StackOrientation.Vertical,
            //                Padding = new Thickness(15, 10, 15, 10),
            //                Margin = new Thickness(0, 0, 0, 0),
            //                Children = {
            //                    new StackLayout
            //                    {
            //                        Spacing = 0,
            //                        Orientation = StackOrientation.Horizontal,
            //                        Padding = new Thickness(0, 5, 0, 0),
            //                        Margin = new Thickness(0, -6, 0, 0),
            //                        HorizontalOptions = LayoutOptions.FillAndExpand,
            //                        Children = {
            //                            new Label {
            //                            Text = "rest time",
            //                            TextTransform = TextTransform.Uppercase,
            //                            FontSize = 14,
            //                            FontFamily = "M",
            //                            TextColor = (Color)App.Current.Resources["AshGrey"],
            //                            HorizontalOptions = LayoutOptions.StartAndExpand,
            //                            Margin = new Thickness(0, 0, 0, 0),
            //                            Padding = new Thickness(0, 0, 0, 0)
            //                            },
            //                            new Label{
            //                            Text = "NO TIME",
            //                            FontSize = 14,
            //                            FontFamily = "M",
            //                            TextColor = (Color)App.Current.Resources["AshGrey"],
            //                            HorizontalOptions = LayoutOptions.End,
            //                            Margin = new Thickness(0, 0, 0, 0),
            //                            Padding = new Thickness(0, 0, 0, 0),
            //                            }
            //                        }
            //                    },
            //                    new StackLayout
            //                    {
            //                        Spacing = 0,
            //                        HorizontalOptions = LayoutOptions.FillAndExpand,
            //                        Padding = new Thickness(0, 20, 0, 0),
            //                        Margin = new Thickness(0, -6, 0, 0),
            //                        Children = {
            //                            new Slider
            //                            {
            //                                Maximum = 14,
            //                                Minimum = 0,
            //                                Value = 0,
            //                                Scale = 2,
            //                                MinimumTrackColor = Color.White,
            //                                MaximumTrackColor = (Color)App.Current.Resources["AshGrey"],
            //                                ThumbColor = (Color)App.Current.Resources["AshGrey"],
            //                                Margin = new Thickness(64, 0, 64, 0)
            //                            }
            //                        }
            //                    }
            //                }

            //            }
            //}
            //    };

            //        TitleSetOne.IsVisible = true;
            //        ParentSetInstance.Children.Add(stackLayout);
            //}
            //}



            //else
            //{

            //        TitleSetOne.IsVisible = false;
            //}



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

        void Handle_SliderNrReps2(object sender, EventArgs e)
        {
            int value = (int)Math.Round(Slider_NrReps2.Value);

            Val_NrReps2.Text = RepsList2[value].Item2;

        }

        void Handle_SliderValRest2(object sender, EventArgs e)
        {
            int value = (int)Math.Round(Slider_Rest2.Value);

            Val_Rest2.Text = RestList2[value].Item2;

        }

    }
}
