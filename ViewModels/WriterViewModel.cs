using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Media_Database.ViewModels
{
    public class WriterViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("Writer Name")]
        public string Name { get; set; } = string.Empty;

        //Connection to Movie and Episode
        [DisplayName("Movies")]
        public List<Guid> SelectedMovieIds { get; set; } = new();

        [DisplayName("Episodes")]
        public List<Guid> SelectedEpisodeIds { get; set; } = new();

        public IEnumerable<SelectListItem> Movies { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Episodes { get; set; } = new List<SelectListItem>();
    }
}
