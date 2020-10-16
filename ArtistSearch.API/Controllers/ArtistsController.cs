using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ArtistSearch.API.Dtos;
using ArtistSearch.BusinessLogic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArtistSearch.API.Controllers
{
    [ApiController, Route("api/artists")]
    public class ArtistsController : ControllerBase
    {


        ILogger<ArtistsController> _logger;
        IMapper _mapper;


        ISearchService _searchService;

        public ArtistsController(ILogger<ArtistsController> logger, ISearchService searchService, IMapper mapper)
        {
            _logger = logger;
            _searchService = searchService;
            _mapper = mapper;
        }

        /// <summary>
        /// Search for an artist
        /// </summary>
        /// <remarks>
        /// Search for an artist using Spotify services
        /// </remarks>
        /// <param name="artistName">artist name</param>
        [HttpPost, Route("search")]
        public async Task<IActionResult> Search([FromBody, Required] string artistName)
        {

            _logger.LogInformation("Request recieved with the following prameter: {artistName}", artistName);

            var result = await _searchService.ArtistSearch(artistName);

            if (result == null)
            {
                return Ok("No result");
            }

            var searchResultDto = _mapper.Map<SearchResultDto>(result);

            return Ok(searchResultDto);

        }
    }
}
