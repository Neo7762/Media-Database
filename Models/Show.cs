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
        public string Name { get; set; }
        public string? Synopsis { get; set; }
        public DateOnly? ReleaseYear { get; set; }
        public WatchStatus WatchStatus { get; set; } = WatchStatus.NotStarted;

        //Count length of series from length of each Season in the Show
        public int LengthMinutes
        {
            get
            {
                if (Seasons != null && Seasons.Count > 0)
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

        //Add all genres from all Season in the Show
        public ICollection<Genre>? Genres
        {
            get
            {
                if (Seasons != null && Seasons.Count > 0)
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

        //Calculate start and end dates of the watch time of the Show based on the date watched of the first and last Episode in the Show
        public DateOnly StartWatch
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0) { return Episodes.Min(e => e.WatchDate) ?? default; }
                else { return default; }
            }
        }
        public DateOnly EndWatch
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0) { return Episodes.Max(e => e.WatchDate) ?? default; }
                else { return default; }
            }
        }

        //Calculate rating and rewatchability of the season based on the average of all episodes
        public int? Rating
        {
            get
            {
                if (Seasons != null && Seasons.Count > 0)
                {
                    return (int?)Seasons.Average(e => e.Rating);
                }
                else
                {
                    return null;
                }
            }
        }
        public int? Rewatchability
        {
            get
            {
                if (Seasons != null && Seasons.Count > 0)
                {
                    return (int?)Seasons.Average(e => e.Rewatchability);
                }
                else
                {
                    return null;
                }
            }
        }

        //Connection to Seasons and Episodes
        public ICollection<Season>? Seasons { get; set; }
        public ICollection<Episode>? Episodes
        {
            get
            {
                if (Seasons != null && Seasons.Count > 0)
                {
                    return Seasons.SelectMany(s => s.Episodes).ToList();
                }
                else
                {
                    return new List<Episode>();
                }
            }
        }

        //Connection to Actor, Director and Writer
        public ICollection<Actor>? Actors { get; set; }
        public ICollection<Director>? Directors { get; set; }
        public ICollection<Writer>? Writers { get; set; }
    }
}
