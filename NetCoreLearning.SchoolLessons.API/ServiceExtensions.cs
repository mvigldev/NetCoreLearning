using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreLearning.Infrastructure.EventBus.Database;
using NetCoreLearning.SchoolLessons.API.Database;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using NetCoreLearning.Infrastructure.Authentication.JWT;
using NetCoreLearning.Infrastructure.Authentication.JWT.Requirements;
using NetCoreLearning.Infrastructure.Authentication.JWT.Handlers;

namespace NetCoreLearning.SchoolLessons.API
{
    /// <summary>
    /// Services Extension methods
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds the JWT authentication, 1 <see cref="MinimumAgeRequirement"/> policy .
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenManagement>(configuration.GetSection("tokenManagement"));
            var token = configuration.GetSection("tokenManagement").Get<TokenManagement>();
            var secret = Encoding.ASCII.GetBytes(token.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromSeconds(1)
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Over18", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new MinimumAgeRequirement(18));
                });

            });
            services.AddScoped<IAuthenticateService, TokenAuthenticationService>();
            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            return services;
        }


        /// <summary>
        /// Adds the custom swagger to the services.
        /// </summary>
        /// <param name="services">The services collection.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerForAPI(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "School Lessons API",
                    Description = "Test API with ASP.NET Core 2,2",
                    TermsOfService = "None",
                    Contact = new Contact()
                    {
                        Name = "Name",
                        Email = "nomail@nomail.com",
                        Url = "www.Url.com"
                    },
                    License = new Swashbuckle.AspNetCore.Swagger.License
                    {
                        Name = "NN",
                        Url = "www.Url.com"
                    },
                });
            });

            return services;
        }

        /// <summary>
        /// Adds the <see cref="SchoolLessonsContext"/>to the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddSchoolLessonsDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SchoolLessonsContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            }
            , ServiceLifetime.Transient);

            return services;
        }


        /// <summary>
        /// Adds the <see cref="EventLogContext"/>to the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddEventBusLoggingDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventLogContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                         sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });

                // Changing default behavior when client evaluation occurs to throw. 
                // Default in EF Core would be to log a warning when client evaluation is performed.
                options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                //Check Client vs. Server evaluation: https://docs.microsoft.com/en-us/ef/core/querying/client-eval
            }
            , ServiceLifetime.Singleton);

            return services;
        }
    }
}
