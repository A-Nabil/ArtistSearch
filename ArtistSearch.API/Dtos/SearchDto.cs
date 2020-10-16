
using System.ComponentModel.DataAnnotations;

namespace ArtistSearch.API.Dtos
{
    public class SearchDto
    {

        [Required]
        public string SearchText { get; set; }
    }
}
