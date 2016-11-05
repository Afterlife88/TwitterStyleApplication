using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TwitterStyleApplication.Domain.Entities;
using TwitterStyleApplication.Web.TokenProvider;

namespace TwitterStyleApplication.Web
{
	public partial class Startup
	{
		// The secret key every token will be signed with.
		// Keep this safe on the server!
		private static readonly string secretKey = "tS6rP8Q5yz78Fdlkscg96Gj5TCI0Vsfl";

		private void ConfigureAuth(IApplicationBuilder app, IServiceProvider services)
		{

			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

			app.UseSimpleTokenProvider(new TokenProviderOptions
			{
				Path = "/api/token",
				Audience = "ExampleAudience",
				Issuer = "ExampleIssuer",
				SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
				IdentityResolver = GetIdentity
			}, services);

			var tokenValidationParameters = new TokenValidationParameters
			{
				// The signing key must match!
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,

				// Validate the JWT Issuer (iss) claim
				ValidateIssuer = true,
				ValidIssuer = "ExampleIssuer",

				// Validate the JWT Audience (aud) claim
				ValidateAudience = true,
				ValidAudience = "ExampleAudience",

				// Validate the token expiry
				ValidateLifetime = true,

				// If you want to allow a certain amount of clock drift, set that here:
				ClockSkew = TimeSpan.Zero
			};

			app.UseJwtBearerAuthentication(new JwtBearerOptions
			{
				AutomaticAuthenticate = true,
				AutomaticChallenge = false,
				TokenValidationParameters = tokenValidationParameters
			});

		}

		private async Task<ClaimsIdentity> GetIdentity(string username, string password, IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
			var user = await userManager.FindByEmailAsync(username);

			if (user != null && await userManager.CheckPasswordAsync(user, password))
			{
				return await Task.FromResult(new ClaimsIdentity(new GenericIdentity(username, "Token"), new Claim[] { }));
			}

			// Credentials are invalid, or account doesn't exist
			return await Task.FromResult<ClaimsIdentity>(null);
		}
	}
}

