using ArtistSearch.API.Settings;
using ArtistSearch.BusinessLogic;
using ArtistSearch.Infrastructure;
using ArtistSearch.Infrastructure.ExternalServices;
using ArtistSearch.Infrastructure.Settings;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ArtistSearch.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            mappingConfig.AssertConfigurationIsValid();

            services.AddMemoryCache();
            services.AddControllers();
            services.AddHttpClient();

            services.AddSingleton<IInfrastructureSettings, InfrastructureSettings>();

            services.AddSingleton<ISpotifyAuth, SpotifyAuth>();

            services.AddTransient<ISearchClient, SpotifyClient>();
            services.AddTransient<ISearchService, SearchService>();

            //log to console
            services.AddLogging(configure => configure.AddConsole());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Use error controller on the production environment
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
