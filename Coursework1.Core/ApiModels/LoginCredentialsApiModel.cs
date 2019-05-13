namespace Coursework1.Core
{
    /// <summary>
    /// The credentials for API client to log into the server and receive a token back
    /// </summary>
    public class LoginCredentialsApiModel
    {
        #region Public Properties

        public string UsernameOrEmail { get; set; }

        public string Password { get; set; }

        #endregion
    }
}
