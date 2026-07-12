using System.ComponentModel.DataAnnotations.Schema;

namespace Media_Database.Models
{
    public class Season
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Title { get; set; }
        public string? Synopsis { get; set; }
        public DateOnly? ReleaseYear { get; set; }

        //Count length of series from length of each Episode in the season
        [NotMapped]
        public int LengthMinutes
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0)
                {
                    int totalMinutes = Episodes.Sum(e => e.LengthMinutes);
                    return (totalMinutes);
                }
                else
                {
                    return 0;
                }
            }
        }

        //Add all genres from all Episodes in the Season
        [NotMapped]
        public ICollection<Genre>? Genres
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0)
                {
                    var genres = Episodes.SelectMany(e => e.Genres).Distinct().ToList();
                    return genres;
                }
                else
                {
                    return new List<Genre>();
                }
            }
        }

        //Calculate start and end dates of the watch time of the Season based on the date watched of the first and last Episode
        [NotMapped]
        public DateOnly StartWatch
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0) { return Episodes.Min(e => e.WatchDate) ?? default; }
                else { return default; }
            }
        }
        [NotMapped]
        public DateOnly EndWatch
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0) { return Episodes.Max(e => e.WatchDate) ?? default; }
                else { return default; }
            }
        }

        //Calculate rating and rewatchability of the Season based on the average of all Episodes
        [NotMapped]
        public int? Rating
        {
            get
            {
                var rated = Episodes?
                    .Where(e => e.Rating.HasValue)
                    .Select(e => e.Rating!.Value)
                    .ToList();

                if (rated == null || rated.Count == 0)
                    return null;

                return (int)Math.Round(rated.Average(), MidpointRounding.AwayFromZero);
            }
        }
        [NotMapped]
        public int? Rewatchability
        {
            get
            {
                var rated = Episodes?
                    .Where(e => e.Rewatchability.HasValue)
                    .Select(e => e.Rewatchability!.Value)
                    .ToList();

                if (rated == null || rated.Count == 0)
                    return null;

                return (int)Math.Round(rated.Average(), MidpointRounding.AwayFromZero);
            }
        }

        //Connection to Episode
        public ICollection<Episode> Episodes { get; set; }

        //Connection to Actor, Director and Writer
        public ICollection<Actor> Actors { get; set; }
        public ICollection<Director> Directors { get; set; }
        public ICollection<Writer> Writers { get; set; }
    }
}
