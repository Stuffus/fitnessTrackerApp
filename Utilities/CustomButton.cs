//The custom button functionality that will be used for navigation
using System.Windows;
using System.Windows.Controls;

namespace fitnessTrackerApp.Utilities
{
    public class CustomButton : Button
    {
        static CustomButton()
        {
            //This static constructor overrides the default style key for the control
            DefaultStyleKeyProperty.OverrideMetadata(
            typeof(CustomButton),
            new FrameworkPropertyMetadata(typeof(CustomButton))
            );
        }
    }
}