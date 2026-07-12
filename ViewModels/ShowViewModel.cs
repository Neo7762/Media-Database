using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Media_Database.ViewModels
{
    public enum WatchStatus
    {
        NotStarted,
        Watching,
        Completed,
        OnHold,
        Dropped
    }
    public class ShowViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Image")]
        public string? ImagePath { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Synopsis")]
        public string? Synopsis { get; set; }
        [Display(Name = "Release Year")]
        public DateOnly? ReleaseYear { get; set; }
        [Display(Name = "Watch Status")]
        public WatchStatus WatchStatus { get; set; } = WatchStatus.NotStarted;

        //Connection to Actor, Director and Writer
        public ICollection<ActorViewModel> Actors { get; set; } = new List<ActorViewModel>();
        public ICollection<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();
        public ICollection<WriterViewModel> Writers { get; set; } = new List<WriterViewModel>();
    }
}
