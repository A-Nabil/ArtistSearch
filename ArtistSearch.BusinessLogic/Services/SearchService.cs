using ArtistSearch.Infrastructure;
using ArtistSearch.Infrastructure.Models;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ArtistSearch.BusinessLogic
{
    public class SearchService : ISearchService
    {
        ISearchClient _iSearchClient;
        public SearchService(ISearchClient iSearchClient)
        {
            _iSearchClient = iSearchClient;
        }

        public async Task<ArtistModel> ArtistSearch(string searchText)
        {

            ArtistModel FoundArtist = null;
            var searchResult = await _iSearchClient.SearchArtist(searchText);


            foreach (var artist in searchResult)
            {
                if (TextCompare(searchText, artist.Name))
                {
                    //Break if the result matches the search query
                    FoundArtist = artist;
                    break;
                }
            }

            return FoundArtist;
        }

        public bool TextCompare(string text1, string text2)
        {
            //Remove pancutation, spaces, and case 
            string regexPattern = @"[\p{P}\s]";
            text1 = Regex.Replace(text1, regexPattern, "").ToLower();
            text2 = Regex.Replace(text2, regexPattern, "").ToLower();

            return text1 == text2;
        }


    }
}
