using System.Windows.Input;

namespace Coursework1.Core
{
    /// <summary>
    /// A view model for text entry to edit a string value
    /// </summary>
    public class TextEntryViewModel : BaseViewModel
    {
        #region Public Properties

        public string Label { get; set; }

        public string OriginalText { get; set; }

        public string EditedText { get; set; }

        public bool Editing { get; set; }

        #endregion

        #region Public Commands

        public ICommand EditCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public ICommand SaveCommand { get; set; }

        #endregion

        #region Default Constructor

        public TextEntryViewModel()
        {
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }

        #endregion

        #region Commands Methods

        private void Save()
        {
            // TODO: Save content
            OriginalText = EditedText;
            Editing = false;
        }

        private void Cancel()
        {
            Editing = false;
        }

        private void Edit()
        {
            Editing = true;
        }

        #endregion
    }
}