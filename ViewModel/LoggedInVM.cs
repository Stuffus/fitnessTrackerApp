using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace fitnessTrackerApp.ViewModel
{
    class LoggedInVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public ICommand LogoutCommand => new RelayCommand(_ =>
        {
            IsLoggedIn = false;

            App.Current.Dispatcher.InvokeAsync(() =>
            {
                var nav = App.Current.MainWindow.DataContext as NavigationVM;
                nav?.HomeCommand.Execute(null); 
            });
        });

        public int UserID
        {
            get => _pageModel.userID;
            set => _pageModel.userID = value;
        }

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

        public LoggedInVM(PageModel pageModel)
        {
            _pageModel = App.SharedPageModel;

            //redirect user to the login page if isLoggedIn value is false(user not logged in)
            if (!_pageModel.isLoggedIn)
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    var nav = App.Current.MainWindow.DataContext as NavigationVM;
                    nav?.HomeCommand.Execute(null);
                });

                return;
            }

            CurrentPage = "Logged In";
        }

    }
}
