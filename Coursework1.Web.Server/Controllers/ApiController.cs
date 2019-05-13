using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Coursework1.Core;
using Coursework1.Core.ApiModels;
using Coursework1.Web.Server.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Coursework1.Web.Server.Controllers
{
    public class ApiController : Controller
    {
        #region Protected Members

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected ApplicationDbContext mContext;

        /// <summary>
        /// The manager for handling user creation, deletion, searching, roles etc...
        /// </summary>
        protected UserManager<ApplicationUser> mUserManager;

        /// <summary>
        /// The manager for handling in and out for our users
        /// </summary>
        protected SignInManager<ApplicationUser> mSignInManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public ApiController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            mContext = context;
            mUserManager = userManager;
            mSignInManager = signInManager;
        }

        #endregion

        #region Login / Register / Verify

        /// <summary>
        /// Tries to register for a new account on the server
        /// </summary>
        /// <param name="registerCredentials">The registration details</param>
        /// <returns>Returns the result of the register request</returns>
        //[AllowAnonymous]
        //[Route(ApiRoutes.Register)]
        //public async Task<ApiResponse<RegisterResultApiModel>> RegisterAsync([FromBody]RegisterCredentialsApiModel registerCredentials)
        //{
        //    // TODO: Localize all strings
        //    // The message when we fail to login
        //    var invalidErrorMessage = "Please provide all required details to register for an account";

        //    // The error response for a failed login
        //    var errorResponse = new ApiResponse<RegisterResultApiModel>
        //    {
        //        // Set error message
        //        ErrorMessage = invalidErrorMessage
        //    };

        //    // If we have no credentials...
        //    if (registerCredentials == null)
        //        // Return failed response
        //        return errorResponse;

        //    // Make sure we have a user name
        //    if (string.IsNullOrWhiteSpace(registerCredentials.Username))
        //        // Return error message to user
        //        return errorResponse;

        //    // Create the desired user from the given details
        //    var user = new ApplicationUser
        //    {
        //        UserName = registerCredentials.Username,
        //        FirstName = registerCredentials.FirstName,
        //        LastName = registerCredentials.LastName,
        //        Email = registerCredentials.Email
        //    };

        //    // Try and create a user
        //    var result = await mUserManager.CreateAsync(user, registerCredentials.Password);

        //    // If the registration was successful...
        //    if (result.Succeeded)
        //    {
        //        // Get the user details
        //        var userIdentity = await mUserManager.FindByNameAsync(user.UserName);

        //        // Send email verification
        //        await SendUserEmailVerificationAsync(user);

        //        // Return valid response containing all users details
        //        return new ApiResponse<RegisterResultApiModel>
        //        {
        //            Response = new RegisterResultApiModel
        //            {
        //                FirstName = userIdentity.FirstName,
        //                LastName = userIdentity.LastName,
        //                Email = userIdentity.Email,
        //                Username = userIdentity.UserName,
        //                Token = userIdentity.GenerateJwtToken()
        //            }
        //        };
        //    }
        //    // Otherwise if it failed...
        //    else
        //        // Return the failed response
        //        return new ApiResponse<RegisterResultApiModel>
        //        {
        //            // Aggregate all errors into a single error string
        //            ErrorMessage = result.Errors.AggregateErrors()
        //        };
        //}

        /// <summary>
        /// Logs in a user using token-based authentication
        /// </summary>
        /// <returns></returns>
        [Route("api/login")]
        public async Task<ApiResponse<LoginResultApiModel>> LogInAsync([FromBody]LoginCredentialsApiModel loginCredentials)
        {
            // TODO: Localize all strings
            // The message when we fail to login
            var invalidErrorMessage = "Invalid username or password";

            // The error response for a failed login
            var errorResponse = new ApiResponse<LoginResultApiModel>
            {
                // Set error message
                ErrorMessage = invalidErrorMessage
            };

            // Make sure we have a user name
            if (loginCredentials?.UsernameOrEmail == null || string.IsNullOrWhiteSpace(loginCredentials.UsernameOrEmail))
                // Return error message to user
                return errorResponse;

            // Validate if the user credentials are correct...

            // Is it an email?
            var isEmail = loginCredentials.UsernameOrEmail.Contains("@");

            // Get the user details
            var user = isEmail ?
                // Find by email
                await mUserManager.FindByEmailAsync(loginCredentials.UsernameOrEmail) :
                // Find by username
                await mUserManager.FindByNameAsync(loginCredentials.UsernameOrEmail);

            // If we failed to find a user...
            if (user == null)
                // Return error message to user
                return errorResponse;

            // If we got here we have a user...
            // Let's validate the password

            // Get if password is valid
            var isValidPassword = await mUserManager.CheckPasswordAsync(user, loginCredentials.Password);

            // If the password was wrong
            if (!isValidPassword)
                // Return error message to user
                return errorResponse;

            // If we get here, we are valid and the user passed the correct login details

            // Get username
            var username = user.UserName;

            // Set our tokens claims
            var claims = new[]
            {
                // Unique ID for this token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                // The username using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, username),
            };

            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(IoCContainer.Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            // Generate the Jwt Token
            var token = new JwtSecurityToken(
                issuer: IoCContainer.Configuration["Jwt:Issuer"],
                audience: IoCContainer.Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                // Expire if not used for 3 months
                expires: DateTime.Now.AddMonths(3)
            );

            // Return token to user
            return new ApiResponse<LoginResultApiModel>
            {
                // Pass back the user details and the token
                Response = new LoginResultApiModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                }
            };
        }

        //[AllowAnonymous]
        //[Route(ApiRoutes.VerifyEmail)]
        //[HttpGet]
        //public async Task<ActionResult> VerifyEmailAsync(string userId, string emailToken)
        //{
        //    // Get the user
        //    var user = await mUserManager.FindByIdAsync(userId);

        //    // If the user is null
        //    if (user == null)
        //        // TODO: Nice UI
        //        return Content("User not found");

        //    // If we have the user...

        //    // Verify the email token
        //    var result = await mUserManager.ConfirmEmailAsync(user, emailToken);

        //    // If succeeded...
        //    if (result.Succeeded)
        //        // TODO: Nice UI
        //        return Content("Email Verified :)");

        //    // TODO: Nice UI
        //    return Content("Invalid Email Verification Token :(");
        //}

        #endregion
        /// <summary>
        /// Test private area for token-based authentication
        /// </summary>
        /// <returns></returns>
        [AuthorizeToken]
        [Route("api/private")]
        public IActionResult Private()
        {
            // Get the authenticated user
            var user = HttpContext.User;

            // Tell them a secret
            return Ok(new { privateData = $"some secret for {user.Identity.Name}" });
        }
    }
}
