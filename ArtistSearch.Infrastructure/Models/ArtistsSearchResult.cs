
using ArtistSearch.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public partial class ArtistsSearchResult
{
    [JsonPropertyName("artists")]
    public Artists SpotifyArtistsSearchResult { get; set; }


    /// <summary>
    /// This function is responsable for extracting the Artist model from SpotifyArtistsSearchResult
    /// I'm not using the Automapper here as this logic is more related to the Spotify client
    /// </summary>
    public List<ArtistModel> GetArtistsNamesList()
    {
        List<ArtistModel> artists = new List<ArtistModel>();

        foreach (var artist in SpotifyArtistsSearchResult.Items)
        {
            ArtistModel ar = new ArtistModel();
            ar.Id = artist.Id;
            ar.Name = artist.Name;
            artists.Add(ar);
        }
        return artists;
    }
}

public partial class Artists
{
    [JsonPropertyName("href")]
    public Uri Href { get; set; }

    [JsonPropertyName("items")]
    public Item[] Items { get; set; }

    [JsonPropertyName("limit")]
    public long Limit { get; set; }

    [JsonPropertyName("next")]
    public object Next { get; set; }

    [JsonPropertyName("offset")]
    public long Offset { get; set; }

    [JsonPropertyName("previous")]
    public object Previous { get; set; }

    [JsonPropertyName("total")]
    public long Total { get; set; }
}

public partial class Item
{
    [JsonPropertyName("external_urls")]
    public ExternalUrls ExternalUrls { get; set; }

    [JsonPropertyName("followers")]
    public Followers Followers { get; set; }

    [JsonPropertyName("genres")]
    public string[] Genres { get; set; }

    [JsonPropertyName("href")]
    public Uri Href { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("images")]
    public Image[] Images { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("popularity")]
    public long Popularity { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("uri")]
    public string Uri { get; set; }
}

public partial class ExternalUrls
{
    [JsonPropertyName("spotify")]
    public Uri Spotify { get; set; }
}

public partial class Followers
{
    [JsonPropertyName("href")]
    public object Href { get; set; }

    [JsonPropertyName("total")]
    public long Total { get; set; }
}

public partial class Image
{
    [JsonPropertyName("height")]
    public long Height { get; set; }

    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("width")]
    public long Width { get; set; }
}






