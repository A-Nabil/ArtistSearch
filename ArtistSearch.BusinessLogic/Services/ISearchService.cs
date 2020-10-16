using ArtistSearch.Infrastructure.Models;
using System.Threading.Tasks;

namespace ArtistSearch.BusinessLogic
{
    public interface ISearchService
    {
        Task<ArtistModel> ArtistSearch(string searchText);
    }
}