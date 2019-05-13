namespace Coursework1.Core
{
    /// <summary>
    /// The response for all Web Api calls made
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        #region Public Properties

        /// <summary>
        /// Indicates if the Api calls were successful
        /// </summary>
        public bool Successful => ErrorMessage == null;

        /// <summary>
        /// The error message for a failed Api call
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// The Api response object
        /// </summary>
        public T Response { get; set; }

        #endregion
    }
}
