using System.IO;
using Media_Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Data
{
    public class MediaContext : DbContext
    {
        public MediaContext(DbContextOptions<MediaContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Show> Shows { get; set; }

    }
}
