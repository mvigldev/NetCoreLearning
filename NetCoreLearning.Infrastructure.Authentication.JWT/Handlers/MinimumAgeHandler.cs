using Microsoft.AspNetCore.Authorization;
using NetCoreLearning.Infrastructure.Authentication.JWT.Requirements;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreLearning.Infrastructure.Authentication.JWT.Handlers
{
    /// <summary>
    /// The handler to be executed for the user when the policy is applied.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization" />
    public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>A <see cref="Task"/> with the decision.</returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(
                c => c.Type == ClaimTypes.DateOfBirth).Value);

            int calculatedAge = DateTime.Today.Year - dateOfBirth.Year;// validate issuer also c.Issuer...

            if (dateOfBirth > DateTime.Today.AddYears(-calculatedAge))
            {
                calculatedAge--;
            }

            if (calculatedAge >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

    }
}
