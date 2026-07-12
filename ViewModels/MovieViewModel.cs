using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Media_Database.ViewModels
{
    public class MovieViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Image")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Synopsis")]
        public string? Synopsis { get; set; }
        [DisplayName("Release Year")]
        public DateOnly? ReleaseYear { get; set; }
        [DisplayName("Length (minutes)")]
        public int LengthMinutes { get; set; }
        [DisplayName("Country of Origin")]
        public string? CountryOfOrigin { get; set; }

        //Connection to Genre
        [DisplayName("Genre")]
        public List<Guid> SelectedGenreIds { get; set; } = new();

        public IEnumerable<SelectListItem> Genres { get; set; } = new List<SelectListItem>();

        //Rating System
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

        //Connections to Actor, Director and Writer
        public List<Guid> SelectedActorIds { get; set; } = new();

        public IEnumerable<SelectListItem> Actors { get; set; } = new List<SelectListItem>();
        public List<Guid> SelectedDirectorIds { get; set; } = new();

        public IEnumerable<SelectListItem> Directors { get; set; } = new List<SelectListItem>();
        public List<Guid> SelectedWriterIds { get; set; } = new();

        public IEnumerable<SelectListItem> Writers { get; set; } = new List<SelectListItem>();
    }
}
