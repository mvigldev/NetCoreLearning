namespace NetCoreLearning.Infrastructure.Authentication.JWT
{
    /// <summary>
    /// Configuration json object
    /// </summary>
    public class TokenManagement
    {
        /// <summary>
        ///     Gets or sets the Microsoft.IdentityModel.Tokens.SecurityKey that is to be used
        ///     for signature validation.       
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        ///  Gets or sets a System.String that represents a valid issuer that will be used
        ///     to check against the token's issuer.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets a string that represents a valid audience that will be used to check
        ///     against the token's audience.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets or sets the access expiration in absolute expiration mode.
        /// </summary>
        public int AccessExpiration { get; set; }
    }
}
