using fitnessTrackerApp.ViewModel;
using System.Windows.Controls;

namespace fitnessTrackerApp.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            DataContext = new HomeVM(App.SharedPageModel);
        }
    }
}
