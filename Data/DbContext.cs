using Media_Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Data
{
    public class MediaContext : DbContext
    {
        public MediaContext(DbContextOptions<MediaContext> options) : base(options) { }

        public DbSet<MovieViewModel> Movies { get; set; }
        public DbSet<GenreViewModel> Genres { get; set; }
        public DbSet<DirectorViewModel> Directors { get; set; }
        public DbSet<WriterViewModel> Writers { get; set; }
        public DbSet<ActorViewModel> Actors { get; set; }
        public DbSet<CollectionViewModel> Collections { get; set; }
        public DbSet<EpisodeViewModel> Episodes { get; set; }
        public DbSet<SeasonViewModel> Seasons { get; set; }
        public DbSet<ShowViewModel> Shows { get; set; }

    }
}
