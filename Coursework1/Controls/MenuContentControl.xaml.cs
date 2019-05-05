using System.Windows.Controls;
using Coursework1.Core;

namespace Coursework1
{
    /// <summary>
    /// Interaction logic for MenuContentControl.xaml
    /// </summary>
    public partial class MenuContentControl : UserControl
    {
        public MenuContentControl()
        {
            InitializeComponent();

            DataContext = IoC.MenuContent;
        }
    }
}
