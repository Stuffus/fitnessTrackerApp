//viewmodel that handles navigation
using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System.Windows.Input;

namespace fitnessTrackerApp.ViewModel
{
    class NavigationVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        private object _currentView;
        public object CurrentView 
        { 
            get { return _currentView; } 
            set { _currentView = value; OnPropertyChanged(); } 
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            set { _isLoggedIn = value; OnPropertyChanged(); }
        }

        public ICommand CompletedWorkoutsCommand { get; set; }
        public ICommand CurrentWorkoutCommand { get; set; } 
        public ICommand HomeCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LoggedInCommand { get; set; }

        private void CompletedWorkouts(object obj) => CurrentView = new CompletedWorkoutsVM(_pageModel);
        private void CurrentWorkout(object obj) => CurrentView = new CurrentWorkoutVM(_pageModel);
        private void Home(object obj) { CurrentView = new HomeVM(_pageModel); }
        private void Settings(object obj) => CurrentView = new SettingsVM(_pageModel);
        public void LoggedIn(object obj) => CurrentView = new LoggedInVM(_pageModel);

        public NavigationVM() 
        {
            _pageModel = App.SharedPageModel;

            CompletedWorkoutsCommand = new RelayCommand(CompletedWorkouts);
            CurrentWorkoutCommand = new RelayCommand(CurrentWorkout);
            HomeCommand = new RelayCommand(Home);
            SettingsCommand = new RelayCommand(Settings);
            LoggedInCommand = new RelayCommand(LoggedIn);
           
            //Startup Page
            CurrentView = new HomeVM(_pageModel);
        }

    }
}
