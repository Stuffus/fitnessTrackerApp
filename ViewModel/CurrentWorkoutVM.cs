using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace fitnessTrackerApp.ViewModel
{
    class CurrentWorkoutVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand WorkoutCommand { get; }
        public ICommand ExerciseCommand { get; }

        public bool IsLoggedIn
        {
            get => _pageModel.isLoggedIn;
            set { _pageModel.isLoggedIn = value; OnPropertyChanged(); }
        }
        public String CurrentPage
        {
            get { return _pageModel.currentPage; }
            set { _pageModel.currentPage = value; }
        }


        public int WorkoutID
        {
            get => _pageModel.workoutID;
            set { _pageModel.workoutID = value; OnPropertyChanged(); }
        }
        public int UserID
        {
            get => _pageModel.userID;
            set { _pageModel.userID = value; OnPropertyChanged(); }
        }


        public string WorkoutName
        {
            get => _pageModel.workoutName;
            set { _pageModel.workoutName = value; OnPropertyChanged(); }
        }
        public int SetNumber
        {
            get => _pageModel.CurrentExercise.SetNumber;
            set { _pageModel.CurrentExercise.SetNumber = value; OnPropertyChanged(); }
        }
        public string ExerciseName
        {
            get => _pageModel.CurrentExercise.ExerciseName;
            set { _pageModel.CurrentExercise.ExerciseName = value; OnPropertyChanged(); }
        }
        public string ExerciseDescription
        {
            get => _pageModel.CurrentExercise.ExerciseDescription;
            set { _pageModel.CurrentExercise.ExerciseDescription = value; OnPropertyChanged(); }
        }
        public int RepCount
        {
            get => _pageModel.CurrentExercise.RepCount;
            set { _pageModel.CurrentExercise.RepCount = value; OnPropertyChanged(); }

        }
        public double RepWeight
        {
            get => _pageModel.CurrentExercise.RepWeight;
            set { _pageModel.CurrentExercise.RepWeight = value; OnPropertyChanged(); }
        }

        public bool ShowNotification 
        {
            get => _pageModel.showNotification;
            set { _pageModel.showNotification = value; OnPropertyChanged(); }
        }
        public CurrentWorkoutVM(PageModel pageModel)
        {
            _pageModel = App.SharedPageModel;
            if (!_pageModel.isLoggedIn)
            {
                var nav = App.Current.MainWindow.DataContext as NavigationVM;
                nav?.HomeCommand.Execute(null);
                return;
            }
            WorkoutCommand = new RelayCommand(param => DoWorkout(param));
            ExerciseCommand = new RelayCommand(param => DoExercise(param));
        }

        private bool _showBeginPanel;
        public bool ShowBeginPanel
        {
            get => _showBeginPanel;
            set { _showBeginPanel = value; OnPropertyChanged(); }
        }

        private void DoWorkout(object? parameter)
        {
            if (DatabaseHelper.CreateWorkout(UserID, WorkoutName))
            {
                ShowBeginPanel = !ShowBeginPanel;
                WorkoutID = DatabaseHelper.GetWorkoutID(WorkoutName);
            }
            else
            {
                System.Windows.MessageBox.Show("Error");
            }
        }

        private async void DoExercise(object? parameter)
        {
            try
            {
                if (DatabaseHelper.CreateExercise(WorkoutID, SetNumber, ExerciseName, ExerciseDescription, Convert.ToInt32(RepCount), Convert.ToDouble(RepWeight)))
                {
                    SetNumber += 1;
                    ShowNotification = true;
                    await Task.Delay(2000);
                    ShowNotification = false;

                }
                
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error adding exercise to db: {ex.Message}");
            }

        }

        public ICommand ToggleBeginPanel => new RelayCommand(_ => 
            {
                ShowBeginPanel = !ShowBeginPanel;
                WorkoutName = null;
            }
        );

    }
}
