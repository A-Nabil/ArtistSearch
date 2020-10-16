using ArtistSearch.Infrastructure.Models;
using ArtistSearch.Infrastructure.Settings;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ArtistSearch.Infrastructure.ExternalServices
{
    public class SpotifyAuth : ISpotifyAuth
    {
        HttpClient Client { get; }
        ILogger<SpotifyAuth> _logger;
        public SpotifyAuth(IHttpClientFactory clientFactory, IInfrastructureSettings _infrastructureSettings, ILogger<SpotifyAuth> logger)
        {
            Client = clientFactory.CreateClient("spotifyAuth");
            Client.BaseAddress = new Uri("https://accounts.spotify.com");
            // GitHub API versioning
            var authenticationString = $"{_infrastructureSettings.ClientId}:{_infrastructureSettings.ClientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(Encoding.ASCII.GetBytes(authenticationString));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            _logger = logger;
        }


        string accessToken
        {
            get;
            set;
        }
        DateTime ExpiresIn { get; set; }

        public async Task<string> getAccessToken()
        {
            if (!string.IsNullOrEmpty(accessToken) && !IsExpired())
                return accessToken;

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/token");

            requestMessage.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                                    "application/x-www-form-urlencoded");//CONTENT-TYPE header

            var response = await Client.SendAsync(requestMessage);
            _logger.LogInformation("Token request sent to Uri: {Uri}", requestMessage.RequestUri);

            //If we did not get 200 an exception will be raised and will be caught at the global exception handler.
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var resultResponse = JsonSerializer.Deserialize<AuthenticationResponse>(responseBody);
            accessToken = resultResponse.AccessToken;
            ExpiresIn = DateTime.UtcNow.AddSeconds(resultResponse.ExpiresIn);
            _logger.LogInformation("A new access token received, ExpiresIn: {ExpiresIn}", ExpiresIn);

            return accessToken;
        }

        bool IsExpired()
        {
            if (DateTime.UtcNow > ExpiresIn)
                return true;
            else return false;
        }
    }
}
