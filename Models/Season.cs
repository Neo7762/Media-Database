namespace Media_Database.Models
{
    public class Season
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string? ImagePath { get; set; }
        public string Name { get; set; }
        public string? Synopsis { get; set; }
        public DateOnly? ReleaseYear { get; set; }

        //Count length of series from length of each Episode in the season
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

        //Calculate rating and rewatchability of the Season based on the average of all Episodes
        public int? Rating
        {
            get
            {
                if (Episodes != null && Episodes.Count > 0)
                {
                    return (int?)Episodes.Average(e => e.Rating);
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
                if (Episodes != null && Episodes.Count > 0)
                {
                    return (int?)Episodes.Average(e => e.Rewatchability);
                }
                else
                {
                    return null;
                }
            }
        }

        //Connection to Episode
        public ICollection<Episode>? Episodes { get; set; }

        //Connection to Actor, Director and Writer
        public ICollection<Actor>? Actors { get; set; }
        public ICollection<Director>? Directors { get; set; }
        public ICollection<Writer>? Writers { get; set; }
    }
}
