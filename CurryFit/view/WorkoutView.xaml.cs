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
    public partial class WorkoutView : ContentPage
    {
        public WorkoutView()
        {
            InitializeComponent();
        }
    }
}