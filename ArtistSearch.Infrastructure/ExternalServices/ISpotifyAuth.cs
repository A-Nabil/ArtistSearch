using System.Threading.Tasks;

namespace ArtistSearch.Infrastructure.ExternalServices
{
    public interface ISpotifyAuth
    {
        Task<string> getAccessToken();
    }
}