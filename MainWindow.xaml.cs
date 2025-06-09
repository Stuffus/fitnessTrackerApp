using fitnessTrackerApp.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace fitnessTrackerApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new NavigationVM();
            InitializeComponent();
        }

        //close the application when the top right "off" button is pressed
        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        //method for window dragging
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
