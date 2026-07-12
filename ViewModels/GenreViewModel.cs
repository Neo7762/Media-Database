using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Media_Database.ViewModels
{
    public class GenreViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [DisplayName("Genre Name")]
        public string Name { get; set; }

        //Connection to Movie and Episode
        [DisplayName("Movies")]
        public List<Guid> SelectedMovieIds { get; set; } = new();

        [DisplayName("Episodes")]
        public List<Guid> SelectedEpisodeIds { get; set; } = new();

        public IEnumerable<SelectListItem> Movies { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Episodes { get; set; } = new List<SelectListItem>();
    }
}
