using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Media_Database.Models;

namespace Media_Database.ViewModels
{
    public class ShowViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Image")]
        public string? ImagePath { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Synopsis")]
        public string? Synopsis { get; set; }

        [Display(Name = "Release Year")]
        public DateOnly? ReleaseYear { get; set; }

        [Display(Name = "Watch Status")]
        public WatchStatus WatchStatus { get; set; } = WatchStatus.NotStarted;
    }
}