using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System.Windows.Input;

namespace fitnessTrackerApp.ViewModel
{
    class SettingsVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;

        public ICommand WeightCommand { get; }

        public int UserID
        {
            get => _pageModel.userID;
            set { _pageModel.userID = value; OnPropertyChanged();}
        }
        public double UserWeight 
        {
            get => _pageModel.userWeight;
            set {_pageModel.userWeight = value; OnPropertyChanged(); }   
        }

        public string CurrentPage 
        {
            get => _pageModel.currentPage;
            set {_pageModel.currentPage = value; OnPropertyChanged(); } 
        }

        public bool IsLoggedIn
        {
            get => _pageModel.isLoggedIn;
            set { _pageModel.isLoggedIn = value; OnPropertyChanged(); }
        }

        public SettingsVM(PageModel pageModel) 
        {
            _pageModel = App.SharedPageModel;
            if (!_pageModel.isLoggedIn)
            {
                var nav = App.Current.MainWindow.DataContext as NavigationVM;

                nav?.HomeCommand.Execute(null);
                return;
            }

            CurrentPage = "Settings";
            WeightCommand = new RelayCommand(param => UpdateWeight(param));
            
            
        }

        private async void UpdateWeight(object? parameter) 
        {
            if (UserWeight < 30)
            {
                CurrentPage = "Weight cannot be lower than 30kgs";
            }
            else
            {

                if (DatabaseHelper.SetUserWeight(UserID, UserWeight))
                {
                    CurrentPage = "Weight Updated!";
                }
                else
                {
                    CurrentPage = "Error";
                }

                await Task.Delay(2000);
                CurrentPage = "Settings";
            }
        }

    }
}
