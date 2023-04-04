using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

[assembly: ExportFont("Nunito-SemiBold.ttf", Alias = "N")]
[assembly: ExportFont("Montserrat-SemiBold.ttf", Alias = "M")]
[assembly: ExportFont("Regin_small-Regular.ttf", Alias = "R")]

namespace CurryFit
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "trainingPrograms.db3"));
                }
                return database;
            }
        }
    }
}
