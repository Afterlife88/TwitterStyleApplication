using System;
using FileStorage.Web.TokenProvider;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace TwitterStyleApplication.Web.TokenProvider
{
    public static class TokenProviderAppBuilderExtensions
    {
        /// <summary/>
        /// Adds the <see cref="TokenProviderMiddleware"/> middleware to the specified <see cref="IApplicationBuilder"/>, which enables token generation capabilities.
        /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
        /// <param name="options">A  <see cref="TokenProviderOptions"/> that specifies options for the middleware.</param>
        /// <param name="services"></param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IApplicationBuilder UseSimpleTokenProvider(this IApplicationBuilder app, TokenProviderOptions options, IServiceProvider services)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<TokenProviderMiddleware>(Options.Create(options), services);
        }
    }
}
