namespace Media_Database.Models
{
    public class MovieViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? ImagePath { get; set; }
        public string Title { get; set; }
        public string? Synopsis { get; set; }
        public DateOnly? ReleaseYear { get; set; }
        public int LengthMinutes { get; set; }
        public string? CountryOfOrigin { get; set; }

        //Connection to Genre
        public ICollection<GenreViewModel> Genres { get; set; }

        //Rating System
        public int? Rating { get; set; }
        public int? Rewatchability { get; set; }
        public DateOnly? WatchDate { get; set; }
        public bool? Watched { get; set; }
        public bool? FirstWatch { get; set; }

        //Connections to Actor, Director and Writer
        public ICollection<ActorViewModel> Actors { get; set; }
        public ICollection<DirectorViewModel> Directors { get; set; }
        public ICollection<WriterViewModel> Writers { get; set; }
    }
}
