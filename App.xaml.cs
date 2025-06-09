//This code defines the App class for a WPF application, initializing a shared PageModel,
//setting up the database, and launching the main window when the application starts
using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System.Windows;

namespace fitnessTrackerApp
{
    public partial class App : Application
    {
        public static PageModel SharedPageModel { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SharedPageModel = new PageModel();
            DatabaseHelper.InitializeDatabase();
            DatabaseHelper.PopulateDbIfEmpty();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

    }

}
