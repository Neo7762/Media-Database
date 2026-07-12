using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Media_Database.ViewModels
{
    public class EpisodeViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }

        [Required]
        [DisplayName("Episode Title")]
        public string Title { get; set; }
        [DisplayName("Episode Synopsis")]
        public string? Synopsis { get; set; }
        [DisplayName("Episode Number")]
        public int EpisodeNumber { get; set; }
        [DisplayName("Release Date")]
        public DateOnly? ReleaseDate { get; set; }
        [DisplayName("Length (Minutes)")]
        public int LengthMinutes { get; set; }

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

        //Connection to Season
        [DisplayName("Season")]
        public List<Guid> SelectedSeasonIds { get; set; } = new();

        public IEnumerable<SelectListItem> Seasons { get; set; } = new List<SelectListItem>();

        //Connection to Collection
        [NotMapped]
        public Guid? CollectionId { get; set; }
        public CollectionViewModel? Collection { get; set; }

        //Connection to Actor, Director, and Writer
        public ICollection<ActorViewModel> Actors { get; set; } = new List<ActorViewModel>();
        public ICollection<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();
        public ICollection<WriterViewModel> Writers { get; set; } = new List<WriterViewModel>();
    }
}
