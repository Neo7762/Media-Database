using System.ComponentModel.DataAnnotations.Schema;

namespace Media_Database.Models
{
    public enum WatchStatus
    {
        NotStarted,
        Watching,
        Completed,
        OnHold,
        Dropped
    }

    public class Show
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Synopsis { get; set; }
        public DateOnly? ReleaseYear { get; set; }
        public WatchStatus WatchStatus { get; set; } = WatchStatus.NotStarted;

        // Direct children
        public ICollection<Season> Seasons { get; set; } = new List<Season>();

        // Flattened episodes (derived)
        [NotMapped]
        public ICollection<Episode> Episodes =>
            Seasons.SelectMany(s => s.Episodes).ToList();

        [NotMapped]
        public int LengthMinutes =>
            Episodes.Sum(e => e.LengthMinutes);

        [NotMapped]
        public ICollection<Genre> Genres =>
            Episodes.SelectMany(e => e.Genres).Distinct().ToList();

        [NotMapped]
        public DateOnly? StartWatch =>
            Episodes.Where(e => e.WatchDate.HasValue)
                    .Select(e => e.WatchDate)
                    .Min();

        [NotMapped]
        public DateOnly? EndWatch =>
            Episodes.Where(e => e.WatchDate.HasValue)
                    .Select(e => e.WatchDate)
                    .Max();

        [NotMapped]
        public int? Rating
        {
            get
            {
                var vals = Episodes.Where(e => e.Rating.HasValue)
                                   .Select(e => e.Rating!.Value)
                                   .ToList();
                return vals.Count == 0
                    ? null
                    : (int)Math.Round(vals.Average(), MidpointRounding.AwayFromZero);
            }
        }

        [NotMapped]
        public int? Rewatchability
        {
            get
            {
                var vals = Episodes.Where(e => e.Rewatchability.HasValue)
                                   .Select(e => e.Rewatchability!.Value)
                                   .ToList();
                return vals.Count == 0
                    ? null
                    : (int)Math.Round(vals.Average(), MidpointRounding.AwayFromZero);
            }
        }

        // DERIVED people from seasons/episodes
        [NotMapped]
        public ICollection<Actor> Actors =>
            Episodes.SelectMany(e => e.Actors).Distinct().ToList();

        [NotMapped]
        public ICollection<Director> Directors =>
            Episodes.SelectMany(e => e.Directors).Distinct().ToList();

        [NotMapped]
        public ICollection<Writer> Writers =>
            Episodes.SelectMany(e => e.Writers).Distinct().ToList();
    }
}