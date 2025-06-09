using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace fitnessTrackerApp.ViewModel
{
    class CompletedWorkoutsVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public int UserID
        {
            get => _pageModel.userID;
            set { _pageModel.userID = value; OnPropertyChanged(); }
        }
        public bool IsLoggedIn
        {
            get => _pageModel.isLoggedIn;
            set { _pageModel.isLoggedIn = value; OnPropertyChanged(); }
        }
        public string CurrentPage
        {
            get { return _pageModel.currentPage; }
            set { _pageModel.currentPage = value; }
        }
        public int WorkoutID 
        {
            get { return _pageModel.workoutID;}
            set { _pageModel.workoutID = value; OnPropertyChanged(); }
        }

        public string WorkoutName
        {
            get { return _pageModel.workoutName; }
            set {  _pageModel.workoutName = value; OnPropertyChanged(); }
        }
        public ICommand ShowExercisesCommand { get; }

        public ObservableCollection<Workout> Workouts { get; set; } = new();

        public CompletedWorkoutsVM(PageModel pageModel)
        {
            _pageModel = App.SharedPageModel;

            if (!_pageModel.isLoggedIn)
            {
                var nav = App.Current.MainWindow.DataContext as NavigationVM;
                nav?.HomeCommand.Execute(null);
                return;
            }

            CurrentPage = "All Workouts";
            ShowExercisesCommand = new RelayCommand(param => LoadExercises(param));
        }


        private void LoadExercises(object? param)
        {
            var workoutsFromDb = DatabaseHelper.GetWorkoutsWithExercises(_pageModel.userID);
            Workouts.Clear();

            foreach (var workout in workoutsFromDb)
                Workouts.Add(workout);
        }

    }
}
