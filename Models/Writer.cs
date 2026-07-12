namespace Media_Database.Models
{
    public class Writer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Name { get; set; } = string.Empty;

        // Many-to-many
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}