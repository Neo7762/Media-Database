namespace Media_Database.Models
{
    public class DirectorViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Name { get; set; }

        //Connection to Movie and Episode
        public Guid? MovieId { get; set; }
        public MovieViewModel? Movie { get; set; }

        public Guid? EpisodeId { get; set; }
        public EpisodeViewModel? Episode { get; set; }
    }
}
