using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreLearning.Infrastructure.Authentication.JWT;

namespace NetCoreLearning.SchoolLessons.API.Controllers
{
    /// <summary>
    /// Accepts authentication requests and validates credentials, than in success provides a JWT token.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticateService _authService;
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        /// <exception cref="System.ArgumentNullException">authService</exception>
        public AuthenticationController(IAuthenticateService authService)
        {
            _authService = authService ?? throw new System.ArgumentNullException(nameof(authService));
        }

        /// <summary>
        /// Requests the token.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult RequestToken([FromBody] TokenRequest request)
        {
            if (_authService.IsAuthenticated(request, out string token))
            {
                return Ok(token);
            }

            return BadRequest("Invalid Request");

        }
    }
}
