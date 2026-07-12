namespace Media_Database.Models
{
    public class CollectionViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        //Different media pieces that can be added to a collection
        public ICollection<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
        public ICollection<EpisodeViewModel> Episodes { get; set; } = new List<EpisodeViewModel>();
        public ICollection<SeasonViewModel> Seasons { get; set; } = new List<SeasonViewModel>();
        public ICollection<ShowViewModel> Shows { get; set; } = new List<ShowViewModel>();
    }
}
