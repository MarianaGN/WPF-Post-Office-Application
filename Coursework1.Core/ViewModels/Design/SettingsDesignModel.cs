namespace Coursework1.Core.Design
{
    /// <summary>
    /// The design-time data for a <see cref="SettingsViewModel"/>
    /// </summary>
    public class SettingsDesignModel : SettingsViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the design model
        /// </summary>
        public static SettingsDesignModel Instance => new SettingsDesignModel();

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SettingsDesignModel()
        {
            Name = new TextEntryViewModel {Label = "Name", OriginalText = "Mariana Kovalchuk"};
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "Mariana" };
            Password = new TextEntryViewModel { Label = "Password", OriginalText = "*******" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "mariana@gmail.com" };
        }

        #endregion
    }
}