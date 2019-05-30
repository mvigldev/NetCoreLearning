using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetCoreLearning.Infrastructure.Authentication.JWT
{
    /// <summary>
    /// Token based authentication service implementation utilizing nuget package System.IdentityModel.Tokens.Jwt. Processes a request that contains a token.
    /// </summary>
    /// <seealso cref="NetCoreLearning.Infrastructure.Authentication.JWT.IAuthenticateService" />
    public class TokenAuthenticationService : IAuthenticateService
    {
        private readonly IUserManagementService _userManagementService;
        private readonly TokenManagement _tokenManagement;
        private readonly string dateFormat = "yyyy.MM-dd";

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenAuthenticationService"/> class.
        /// </summary>
        /// <param name="service">The <see cref="IUserManagementService"/> that will be used.</param>
        /// <param name="tokenManagement">The <see cref="TokenManagement"/> hat will be used.</param>
        public TokenAuthenticationService(IUserManagementService service, IOptions<TokenManagement> tokenManagement)
        {
            _userManagementService = service;
            _tokenManagement = tokenManagement.Value;
        }

        /// <summary>
        /// Determines whether the specified request is authenticated and if it is, creates the token with the user claims.
        /// </summary>
        /// <param name="request">The request to process.</param>
        /// <param name="token">The token to validate.</param>
        /// <returns>
        ///   <c>true</c> if the specified request is authenticated; otherwise, <c>false</c>.
        /// </returns>
        public bool IsAuthenticated(TokenRequest request, out string token)
        {
            token = string.Empty;
            if (!_userManagementService.IsValidUser(request.Username, request.Password)) return false;

            var claim = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.DateOfBirth, new DateTime(2000,1,1).ToString(dateFormat))//dummy user
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.UtcNow.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;

        }
    }
}
