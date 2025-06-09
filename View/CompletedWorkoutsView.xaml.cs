using fitnessTrackerApp.ViewModel;
using System.Windows.Controls;

namespace fitnessTrackerApp.View
{
    /// <summary>
    /// Interaction logic for CompletedWorkoutsView.xaml
    /// </summary>
    public partial class CompletedWorkoutsView : UserControl
    {
        public CompletedWorkoutsView()
        {
            InitializeComponent();
            DataContext = new CompletedWorkoutsVM(App.SharedPageModel);
        }
    }
}
