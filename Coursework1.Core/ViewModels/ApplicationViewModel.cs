﻿namespace Coursework1.Core
{
    /// <summary>
    /// The application state as a view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to set the view model of the current page 
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// True if the Setting menu should be shown
        /// </summary>
        public bool SettingsMenuVisible { get; set; } = false;

        /// <summary>
        /// True if the Setting menu should be shown
        /// </summary>
        public bool AddParcelControlVisible { get; set; } = false;

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        /// <param name="page">the page to go to</param>
        public void GoToPage(ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Always hide settings page if we are changing pages
            SettingsMenuVisible = false;

            // Set the view model
            CurrentPageViewModel = viewModel;

            // See if page has changed
            var different = CurrentPage != page;

            // Set the current page
            CurrentPage = page;

            // If the page hasn't changed, fire off notification
            // So pages still update if just the view model has changed
            if (!different)
                OnPropertyChanged(nameof(CurrentPage));

            // Show side menu or not?
            SideMenuVisible = page == ApplicationPage.Content;
        }
    }
}