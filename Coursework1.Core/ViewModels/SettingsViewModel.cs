using System.Windows.Input;

namespace Coursework1.Core
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties

        public TextEntryViewModel Name { get; set; }

        public TextEntryViewModel Username { get; set; }

        public TextEntryViewModel Password { get; set; }

        public TextEntryViewModel Email { get; set; }

        #endregion

        #region Commands

        public ICommand OpenCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        #endregion

        #region Default Constructor

        public SettingsViewModel()
        {
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);

            // Remove in future, add data from real database
            Name = new TextEntryViewModel { Label = "Name", OriginalText = "Mariana Kovalchuk" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "Mariana" };
            Password = new TextEntryViewModel { Label = "Password", OriginalText = "*******" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "mariana@gmail.com" };
        }

        #endregion

        #region Commands Methods

        private void Close()
        {
            IoC.Application.SettingsMenuVisible = false;
        }

        private void Open()
        {
            IoC.Application.SettingsMenuVisible = true;
        }

        #endregion

    }
}