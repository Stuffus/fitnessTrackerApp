//ViewModelBase class implements INotifyPropertyChanged to support property change notifications in MVVM, allowing the UI to update when bound property values change
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace fitnessTrackerApp.Utilities
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propName = null) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
