using System.Windows.Controls;
using Coursework1.Core;

namespace Coursework1
{
    /// <summary>
    /// Interaction logic for SettingControl.xaml
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            DataContext = IoC.Settings;
        }
    }
}
