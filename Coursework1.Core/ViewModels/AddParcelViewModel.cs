using System.Windows.Input;

namespace Coursework1.Core
{
    public class AddParcelViewModel : BaseViewModel
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

        public AddParcelViewModel()
        {
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
        }

        #endregion

        #region Commands Methods

        private void Close()
        {
            IoC.Application.AddParcelControlVisible = false;
        }

        private void Open()
        {
            IoC.Application.AddParcelControlVisible = true;
        }

        #endregion

    }
}