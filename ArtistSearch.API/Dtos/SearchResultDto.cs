using System.Text.Json.Serialization;

namespace ArtistSearch.API.Dtos
{
    public class SearchResultDto
    {
        [JsonPropertyName("artist_id")]
        public string ArtistId { get; set; }


        [JsonPropertyName("artist_name")]
        public string ArtistName { get; set; }

    }
}
