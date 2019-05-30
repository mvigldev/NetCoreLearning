namespace NetCoreLearning.Infrastructure.Authentication.JWT
{
    /// <summary>
    /// Contract for taken based authentication service 
    /// </summary>
    public interface IAuthenticateService
    {
        /// <summary>
        /// Determines whether the specified request is authenticated.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <param name="token">The token to validate.</param>
        /// <returns>
        ///   <c>true</c> if the specified request is authenticated; otherwise, <c>false</c>.
        /// </returns>
        bool IsAuthenticated(TokenRequest request, out string token);
    }
}
