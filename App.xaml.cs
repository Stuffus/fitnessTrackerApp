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
