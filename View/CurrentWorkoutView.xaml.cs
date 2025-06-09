using fitnessTrackerApp.ViewModel;
using System.Windows.Controls;

namespace fitnessTrackerApp.View
{
    /// <summary>
    /// Interaction logic for CurrentWorkoutView.xaml
    /// </summary>
    public partial class CurrentWorkoutView : UserControl
    {
        public CurrentWorkoutView()
        {
            InitializeComponent();
            DataContext = new CurrentWorkoutVM(App.SharedPageModel);
        }
    }
}
