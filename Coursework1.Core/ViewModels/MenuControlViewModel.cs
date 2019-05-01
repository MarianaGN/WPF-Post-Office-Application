using System.Windows.Input;

namespace Coursework1.Core
{
    public class MenuControlViewModel : BaseViewModel
    {
        #region Public Properties


        #endregion

        #region Commands

        public ICommand OpenCommand { get; set; }

        #endregion

        #region Default Constructor

        public MenuControlViewModel()
        {
            OpenCommand = new RelayCommand(Open);
        }

        #endregion

        #region Commands Methods

        private void Open()
        {
            IoC.Application.SettingsMenuVisible = true;
        }

        #endregion

    }
}