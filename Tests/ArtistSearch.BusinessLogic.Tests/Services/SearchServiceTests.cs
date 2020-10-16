using Xunit;
using ArtistSearch.Infrastructure;
using Moq;

namespace ArtistSearch.BusinessLogic.Tests
{
    public class SearchServiceTests
    {
        SearchService _SearchService;

        public SearchServiceTests()
        {
            var _messagingClient = new Mock<ISearchClient>();


            _SearchService = new SearchService(_messagingClient.Object);
        }


        [Fact()]
        public void TextCompare_success()
        {
            var result = _SearchService.TextCompare("Katy Perry", "Katyperry");
            Assert.True(result);

            result = _SearchService.TextCompare("Katy Perry", "katyPERRy");
            Assert.True(result);

            result = _SearchService.TextCompare("Katy Perry", "Katy Perry");
            Assert.True(result);

            result = _SearchService.TextCompare("Katy Perry", "Katy.Perry");
            Assert.True(result);

        }
        [Fact()]
        public void TextCompare_Fail()
        {
            var result = _SearchService.TextCompare("Katy Perry", "The katy perry");
            Assert.False(result);

            result = _SearchService.TextCompare("Katy Perry", "Katy perry 2");
            Assert.False(result);

        }
    }
}