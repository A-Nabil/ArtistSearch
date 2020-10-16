using ArtistSearch.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtistSearch.Infrastructure
{
    public interface ISearchClient
    {
        Task<List<ArtistModel>> SearchArtist(string searchQuery);
    }
}