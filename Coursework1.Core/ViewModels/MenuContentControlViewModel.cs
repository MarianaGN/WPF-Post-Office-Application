using System.Windows.Input;

namespace Coursework1.Core
{
    public class MenuContentControlViewModel : BaseViewModel
    {
        #region Public Properties


        #endregion

        #region Commands

        public ICommand OpenCommand { get; set; }

        #endregion

        #region Default Constructor

        public MenuContentControlViewModel()
        {
            OpenCommand = new RelayCommand(Open);
        }

        #endregion

        #region Commands Methods

        private void Open()
        {
            IoC.Application.GoToPage(ApplicationPage.Content, new AddParcelViewModel());
        }

        #endregion

    }
}