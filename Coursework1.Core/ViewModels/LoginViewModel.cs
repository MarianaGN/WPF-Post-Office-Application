using System.Security;
using System.Threading.Tasks;
using System.Windows.Input;
using Coursework1.Core.ApiModels;
using Dna;

namespace Coursework1.Core
{
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to login
        /// </summary>
        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel()
        {
            // Create commands
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await LoginAsync(parameter));
        }

        #endregion

        /// <summary>
        /// Attempts to log the user in
        /// </summary>
        /// <param name="parameter">The <see cref="SecureString"/> passed in from the view for the users password</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {

            await RunCommandAsync(() => LoginIsRunning, async () =>
            {
                // Call the server and attempt to login with credentials
                // TODO: Move all URLs and API routes to static class in core
                var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>>(
                    "http://localhost:5000/api/login",
                    new LoginCredentialsApiModel
                    {
                        UsernameOrEmail = Email,
                        Password = (parameter as IHavePassword).SecurePassword.Unsecure()
                    });

                // If there was no response, bad data, or a response with a error message...
                if (result == null || result.ServerResponse == null || !result.ServerResponse.Successful)
                {
                    // Default error message
                    // TODO: Localize strings
                    var message = "Unknown error from server call";

                    // If we got a response from the server...
                    if (result?.ServerResponse != null)
                        // Set message to servers response
                        message = result.ServerResponse.ErrorMessage;
                    // If we have a result but deserialize failed...
                    else if (!string.IsNullOrWhiteSpace(result?.RawServerResponse))
                        // Set error message
                        message = $"Unexpected response from server. {result.RawServerResponse}";
                    // If we have a result but no server response details at all...
                    else if (result != null)
                        // Set message to standard HTTP server response details
                        message = $"Failed to communicate with server. Status code {result.StatusCode}. {result.StatusDescription}";

                   

                    // We are done
                    //return;
                }

                // OK successfully logged in... now get users data
                var userData = result.ServerResponse.Response;

                //IoC.Settings.Name = new TextEntryViewModel { Label = "Name", OriginalText = $"{userData.FirstName} {userData.LastName}" };
                //IoC.Settings.Username = new TextEntryViewModel { Label = "Username", OriginalText = userData.Username };
                //IoC.Settings.Email = new TextEntryViewModel { Label = "Email", OriginalText = userData.Email };


                IoC.Get<ApplicationViewModel>().GoToPage(ApplicationPage.Content);
            });
        }
    }
}