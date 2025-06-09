using fitnessTrackerApp.ViewModel;
using System.Windows.Controls;

namespace fitnessTrackerApp.View
{
    /// <summary>
    /// Interaction logic for LoggedInView.xaml
    /// </summary>
    public partial class LoggedInView : UserControl
    {
        public LoggedInView()
        {
            InitializeComponent();
            DataContext = new LoggedInVM(App.SharedPageModel);
        }
    }
}
