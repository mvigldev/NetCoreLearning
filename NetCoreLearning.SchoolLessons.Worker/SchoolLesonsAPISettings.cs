namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// The settings object from app.json
    /// </summary>
    public class SchoolLesonsAPISettings
    {
        /// <summary>
        /// Gets or sets the SchoolLessons API base URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the UserName to connect to the API.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password to connect to the API.
        /// </summary>
        public string Password { get; set; }
    }
}
