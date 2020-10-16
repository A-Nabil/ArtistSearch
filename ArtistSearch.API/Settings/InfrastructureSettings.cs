using ArtistSearch.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;

namespace ArtistSearch.API.Settings
{
    public class InfrastructureSettings : IInfrastructureSettings
    {
        private readonly IConfiguration _config;
        public InfrastructureSettings(IConfiguration config)
        {
            _config = config;
        }

        public string ClientId => _config["SpotifyClientId"];
        public string ClientSecret => _config["SpotifyClientSecret"];
    }
}
