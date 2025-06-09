using fitnessTrackerApp.ViewModel;
using System.Windows.Controls;

namespace fitnessTrackerApp.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            DataContext = new SettingsVM(App.SharedPageModel);
        }
    }
}
