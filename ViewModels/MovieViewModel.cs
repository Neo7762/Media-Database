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
        public ICollection<ActorViewModel> Actors { get; set; } = new List<ActorViewModel>();
        public ICollection<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();
        public ICollection<WriterViewModel> Writers { get; set; } = new List<WriterViewModel>();
    }
}
