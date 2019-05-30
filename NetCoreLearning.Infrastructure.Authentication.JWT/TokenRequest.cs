using System.ComponentModel.DataAnnotations;

namespace NetCoreLearning.Infrastructure.Authentication.JWT
{
    /// <summary>
    /// Model for any API that requests a toke.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        public string Username { get; set; }


        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
