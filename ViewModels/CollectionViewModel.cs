using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Media_Database.ViewModels
{
    public class CollectionViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("Collection Name")]
        public string Name { get; set; }
        [DisplayName("Collection Description")]
        public string? Description { get; set; }

        //Connection to Movie, Episode, Season, and Show
        [DisplayName("Movies")]
        public List<Guid> SelectedMovieIds { get; set; } = new();

        [DisplayName("Episodes")]
        public List<Guid> SelectedEpisodeIds { get; set; } = new();

        [DisplayName("Seasons")]
        public List<Guid> SelectedSeasonIds { get; set; } = new();

        [DisplayName("Shows")]
        public List<Guid> SelectedShowIds { get; set; } = new();

        public IEnumerable<SelectListItem> Movies { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Episodes { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Seasons { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Shows { get; set; } = new List<SelectListItem>();
    }
}
