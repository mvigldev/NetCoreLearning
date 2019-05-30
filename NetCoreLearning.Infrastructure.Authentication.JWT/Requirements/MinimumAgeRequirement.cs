using Microsoft.AspNetCore.Authorization;

namespace NetCoreLearning.Infrastructure.Authentication.JWT.Requirements
{
    /// <summary>
    /// This is an <see cref="IAuthorizationRequirement"/> that represents a minimum age for the user.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumAgeRequirement"/> class.
        /// </summary>
        /// <param name="age">The age to set.</param>
        public MinimumAgeRequirement(int age)
        {
            MinimumAge = age;
        }

        /// <summary>
        /// Gets or sets the minimum age of the user.
        /// </summary>
        public int MinimumAge { get; set; }
    }
}
