using ArtistSearch.Infrastructure.ExternalServices;
using ArtistSearch.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtistSearch.Infrastructure
{
    public class SpotifyClient : ISearchClient
    {
        HttpClient _client { get; }
        ISpotifyAuth _spotifyAuth;

        private IMemoryCache _cache;
        ILogger<SpotifyClient> _logger;

        string BaseUrl = "https://api.spotify.com";
        public SpotifyClient(IHttpClientFactory clientFactory, ISpotifyAuth spotifyAuth, ILogger<SpotifyClient> logger, IMemoryCache cache)
        {
            _client = clientFactory.CreateClient("SpotifyClient");
            _client.BaseAddress = new Uri(BaseUrl);
            _spotifyAuth = spotifyAuth;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<ArtistModel>> SearchArtist(string searchQuery)
        {
            // Check if cache is not empty
            if (_cache.TryGetValue("ArtistSearchResult" + searchQuery, out List<ArtistModel> CachedResult))
            {
                // Return data from the cache
                return CachedResult;
            }

            //get access token
            var token = await _spotifyAuth.getAccessToken();

            string url = $"{BaseUrl}/v1/search?q={Uri.EscapeDataString(searchQuery)}&type=artist";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.SendAsync(requestMessage);
            _logger.LogInformation("Artist search request sent to Uri: {Uri}", requestMessage.RequestUri);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var resultResponse = JsonSerializer.Deserialize<ArtistsSearchResult>(responseBody);

            // Result data will be cached for 2 hours
            _cache.Set("ArtistSearchResult" + searchQuery, resultResponse.GetArtistsNamesList(), TimeSpan.FromHours(2));

            return resultResponse.GetArtistsNamesList();
        }
    }
}
