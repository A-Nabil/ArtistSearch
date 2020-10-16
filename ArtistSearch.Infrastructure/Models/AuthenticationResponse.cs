using System;
using System.Text.Json.Serialization;

namespace ArtistSearch.Infrastructure.Models
{
    public class AuthenticationResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("artist_name")]
        public string ArtistName { get; set; }
    }
}
