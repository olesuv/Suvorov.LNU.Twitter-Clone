using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Suvorov.LNU.TwitterClone.Core.Mapper;
using Suvorov.LNU.TwitterClone.Core.Interfaces;
using Suvorov.LNU.TwitterClone.Database;
using Suvorov.LNU.TwitterClone.Models.Database;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Suvorov.LNU.TwitterClone.Models.Configuration;

namespace Suvorov.LNU.TwitterClone.Web
{
    public class Startup
    {
        private readonly IOptions<GoogleOAuthConfig> _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            services.AddSingleton<IMapperProvider, MapperProvider>();
            services.AddSingleton(GetMapper);

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    options.ClientId = _configuration.Value.ClientId;
                    options.ClientSecret = _configuration.Value.ClientSecret;
                });

            services.AddMvc();

            services.AddControllers();
        }

        public static IMapper GetMapper(IServiceProvider serviceProvider)
        {
            var provider = serviceProvider.GetRequiredService<IMapperProvider>();
            return provider.GetMapper();
        }

        [Obsolete]
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();
        }
    }
}
