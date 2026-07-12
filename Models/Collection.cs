namespace Media_Database.Models
{
    public class Collection
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //Different media pieces that can be added to a collection
        public ICollection<Movie>? Movies { get; set; } = new List<Movie>();
        public ICollection<Episode>? Episodes { get; set; } = new List<Episode>();
        public ICollection<Season>? Seasons { get; set; } = new List<Season>();
        public ICollection<Show>? Shows { get; set; } = new List<Show>();
    }
}
