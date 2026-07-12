using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Media_Database.ViewModels
{
    public class SeasonViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Synopsis")]
        public string? Synopsis { get; set; }

        [DisplayName("Release Year")]
        public DateOnly? ReleaseYear { get; set; }

        // Required for flow: must pick a show first
        [Required]
        [DisplayName("Show")]
        public Guid ShowId { get; set; }
        public IEnumerable<SelectListItem> Shows { get; set; } = new List<SelectListItem>();

        // Optional: show existing episodes for context (not manual linking source-of-truth)
        [DisplayName("Episodes")]
        public List<Guid> SelectedEpisodeIds { get; set; } = new();
        public IEnumerable<SelectListItem> Episodes { get; set; } = new List<SelectListItem>();
    }
}