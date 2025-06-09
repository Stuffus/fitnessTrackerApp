using fitnessTrackerApp.Model;
using fitnessTrackerApp.Utilities;
using System.Reflection.Metadata;
using System.Windows.Controls;
using System.Windows.Input;

namespace fitnessTrackerApp.ViewModel
{
    class HomeVM : Utilities.ViewModelBase
    {
        private readonly PageModel _pageModel;
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

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

        public string Email
        {
            get => _pageModel.email;

            set { _pageModel.email = value; OnPropertyChanged(); }
        }
        public string Login
        {
            get => _pageModel.login;

            set { _pageModel.login = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get => _pageModel.password;
            set { _pageModel.password = value; OnPropertyChanged(); }
        }
        public String CurrentPage
        {
            get => _pageModel.currentPage;
            set { _pageModel.currentPage = value; OnPropertyChanged(); }
        }


        public HomeVM(PageModel pageModel)
        {
            _pageModel = App.SharedPageModel;

            if (_pageModel.isLoggedIn)
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    var nav = App.Current.MainWindow.DataContext as NavigationVM;
                    nav?.LoggedInCommand.Execute(null);
                });

                return;
            }

            CurrentPage = "Log In";

            LoginCommand = new RelayCommand(param => DoLogin(param));
            RegisterCommand = new RelayCommand(param => DoRegister(param));
        }



        private bool _showSignUpPanel;
        public bool ShowSignUpPanel
        {
            get => _showSignUpPanel;
            set { _showSignUpPanel = value; OnPropertyChanged(); }
        }

        public ICommand ToggleSignUpCommand => new RelayCommand(_ => ShowSignUpPanel = !ShowSignUpPanel);

        private void DoLogin(object? parameter)
        {
            if (parameter is PasswordBox passwordBox)
                Password = passwordBox.Password;

            if (DatabaseHelper.ValidateUser(Login, Password)) 
            {
                CurrentPage = "Success";
                IsLoggedIn = true;
                UserID = DatabaseHelper.GetUserID(Login);
                var nav = App.Current.MainWindow.DataContext as NavigationVM;
                nav?.LoggedInCommand.Execute(null);
            }
            else 
            {
                CurrentPage = "Failure";
            }

        }

        private void DoRegister(object? parameter) 
        {
            if (parameter is PasswordBox passwordBox)
                Password = passwordBox.Password;

            if(DatabaseHelper.RegisterUser(Login, Password, Email))
            {
                CurrentPage = "Succesfull Registration";
            }
            else
            {
                CurrentPage = "Failure Registrating, Login and Password must have at least 4 characters";
            }
        }

    }

}
