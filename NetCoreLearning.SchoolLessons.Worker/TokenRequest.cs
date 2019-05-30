namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// Model for any API that requests a toke.
    /// </summary>
    public class TokenRequest
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }


        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
