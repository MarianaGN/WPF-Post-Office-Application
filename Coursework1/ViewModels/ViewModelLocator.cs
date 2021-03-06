﻿using Coursework1.Core;

namespace Coursework1
{
    /// <summary>
    /// Locates view models from the IoC for use in binding in Xaml files
    /// </summary>
    public class ViewModelLocator
    {
        #region Public Properties

        /// <summary>
        /// Singleton instance of the locator
        /// </summary>
        public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// The application view model
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.Application;

        /// <summary>
        /// The settings view model
        /// </summary>
        public static SettingsViewModel SettingsViewModel => IoC.Settings;

        public static AddParcelViewModel AddParcelViewModel => IoC.AddParcel;

        public static MenuContentControlViewModel MenuContentControlViewModel => IoC.MenuContent;

        #endregion
    }
}