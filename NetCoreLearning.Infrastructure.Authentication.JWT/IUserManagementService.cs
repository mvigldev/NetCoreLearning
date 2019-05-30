namespace NetCoreLearning.Infrastructure.Authentication.JWT
{
    /// <summary>
    /// The contract for the user management service for user validations
    /// </summary>
    public interface IUserManagementService
    {
        /// <summary>
        /// Determines whether th provided credentials are valid.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <c>true</c> if the credentials are valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValidUser(string username, string password);
    }
}
