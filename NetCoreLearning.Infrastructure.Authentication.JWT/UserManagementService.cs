namespace NetCoreLearning.Infrastructure.Authentication.JWT
{
    /// <summary>
    /// The service that validates user credentials
    /// </summary>
    /// <seealso cref="NetCoreLearning.Infrastructure.Authentication.JWT.IUserManagementService" />
    public class UserManagementService : IUserManagementService
    {
        /// <summary>
        /// Determines whether a user is valid according to <paramref name="userName"/> and <paramref name="password"/>.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>
        ///   <c>true</c> if user is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValidUser(string userName, string password)
        {
            return true;//todo: implement here logic ...
        }
    }
}
