using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Media_Database.ViewModels
{
    public class SeasonViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Image")]
        public string? ImagePath { get; set; }
        [Required]
        [DisplayName("Title")]
        public string Title { get; set; }
        [DisplayName("Synopsis")]
        public string? Synopsis { get; set; }
        [DisplayName("Release Year")]
        public DateOnly? ReleaseYear { get; set; }

        //Connection to Episode
        public ICollection<EpisodeViewModel> Episodes { get; set; }

        //Connection to Actor, Director and Writer
        public ICollection<ActorViewModel> Actors { get; set; } = new List<ActorViewModel>();
        public ICollection<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();
        public ICollection<WriterViewModel> Writers { get; set; } = new List<WriterViewModel>();
    }
}
