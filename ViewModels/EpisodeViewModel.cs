using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Media_Database.ViewModels
{
    public class EpisodeViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("Episode Title")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Episode Synopsis")]
        public string? Synopsis { get; set; }

        [DisplayName("Episode Number")]
        public int EpisodeNumber { get; set; }

        [DisplayName("Release Date")]
        public DateOnly? ReleaseDate { get; set; }

        [DisplayName("Length (Minutes)")]
        public int LengthMinutes { get; set; }

        [DisplayName("Genre")]
        public List<Guid> SelectedGenreIds { get; set; } = new();
        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();

        [DisplayName("Rating")]
        public int? Rating { get; set; }

        [DisplayName("Rewatchability")]
        public int? Rewatchability { get; set; }

        [DisplayName("Watch Date")]
        public DateOnly? WatchDate { get; set; }

        [DisplayName("Watched")]
        public bool? Watched { get; set; }

        [DisplayName("First Watch")]
        public bool? FirstWatch { get; set; }

        // Required for cascade flow
        [Required]
        [DisplayName("Season")]
        public Guid SeasonId { get; set; }

        // Optional
        [DisplayName("Collection")]
        public Guid? CollectionId { get; set; }
        public IEnumerable<SelectListItem> Collections { get; set; } = new List<SelectListItem>();

        public List<Guid> SelectedActorIds { get; set; } = new();
        public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();

        public List<Guid> SelectedDirectorIds { get; set; } = new();
        public IEnumerable<SelectListItem> Directors { get; set; } = new List<SelectListItem>();

        public List<Guid> SelectedWriterIds { get; set; } = new();
        public IEnumerable<SelectListItem> Writers { get; set; } = new List<SelectListItem>();

        // Needed so user can pick an existing season
        public IEnumerable<SelectListItem> Seasons { get; set; } = new List<SelectListItem>();
    }
}