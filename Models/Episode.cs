using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Media_Database.Models
{
    public class Episode
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Title { get; set; }
        public string? Synopsis { get; set; }
        public int EpisodeNumber { get; set; }
        public DateOnly? ReleaseDate { get; set; }
        public int LengthMinutes { get; set; }

        //Connection to Genre
        public ICollection<Genre> Genres { get; set; } = new List<Genre>();

        //Rating System
        public int? Rating { get; set; }
        public int? Rewatchability { get; set; }
        public DateOnly? WatchDate { get; set; }
        public bool? Watched { get; set; }
        public bool? FirstWatch { get; set; }

        //Connection to Season
        public Guid SeasonId { get; set; }
        public Season Season { get; set; }

        //Connection to Collection
        public Guid? CollectionId { get; set; }
        public Collection? Collection { get; set; }

        //Connection to Actor, Director, and Writer
        public ICollection<Actor> Actors { get; set; } = new List<Actor>();
        public ICollection<Director> Directors { get; set; } = new List<Director>();
        public ICollection<Writer> Writers { get; set; } = new List<Writer>();
    }
}
