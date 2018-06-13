using System;
using Filmiki.Views;
using Filmiki.ViewModels;
using Filmiki.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Filmiki.Data;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Filmiki
{
	public partial class App : Application
	{
        private static DatabaseController _database;

        public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new MoviePage(new MovieViewModel()));
		}

        public static DatabaseController Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new DatabaseController(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("database.db3"));
                }
                return _database;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
