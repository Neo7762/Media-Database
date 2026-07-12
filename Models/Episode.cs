using System.IO;

namespace Media_Database.Models
{
    public class Episode
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public int EpisodeNumber { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int LengthMinutes { get; set; }

        // Rating system (whole-number only)
        public int? Rating { get; set; }
        public int? Rewatchability { get; set; }
        public DateOnly? WatchDate { get; set; }
        public bool? Watched { get; set; }
        public bool? FirstWatch { get; set; }

        // Required parent: Episode -> Season
        public Guid SeasonId { get; set; }
        public Season Season { get; set; } = null!;

        // Optional parent: Episode -> Collection
        public Guid? CollectionId { get; set; }
        public Collection? Collection { get; set; }

        // Many-to-many
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<Director> Directors { get; set; } = new List<Director>();
        public ICollection<Writer> Writers { get; set; } = new List<Writer>();
    }
}