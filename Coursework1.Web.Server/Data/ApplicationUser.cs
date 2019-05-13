using Microsoft.AspNetCore.Identity;

namespace Coursework1.Web.Server
{
    /// <summary>
    /// The user data and profile for our application
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        #region Public Properties

        public string FirstName { get; set; }

        public string LastName { get; set; }

        #endregion
    }
}
