using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Coursework1
{
    public class NoFrameHistory : BaseAttachedProperty<NoFrameHistory, bool>
    {
        public override void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            // Get the frame
            var frame = (sender as Frame);

            // Hide navigation bar
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;

            // Cleat history on navigate
            frame.Navigated += (ss, ee) => ((Frame) ss).NavigationService.RemoveBackEntry();
        }
    }
}