using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class EpisodeService
    {
        private readonly MediaContext _context;

        public EpisodeService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<EpisodeViewModel>> GetAllAsync()
        {
            var episodes = await _context.Episodes
                .Include(e => e.Season)
                .ToListAsync();

            return episodes.Select(e => new EpisodeViewModel
            {
                Id = e.Id,
                ImagePath = e.ImagePath,
                Title = e.Title,
                Synopsis = e.Synopsis,
                EpisodeNumber = e.EpisodeNumber,
                ReleaseDate = e.ReleaseDate,
                LengthMinutes = e.LengthMinutes,
                Rating = e.Rating,
                Rewatchability = e.Rewatchability,
                WatchDate = e.WatchDate,
                Watched = e.Watched,
                FirstWatch = e.FirstWatch,
                SeasonId = e.SeasonId,
                CollectionId = e.CollectionId
            }).ToList();
        }

        public async Task<EpisodeViewModel?> GetForEditAsync(Guid id)
        {
            var e = await _context.Episodes
                .Include(x => x.Genres)
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Include(x => x.Writers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (e == null) return null;

            return new EpisodeViewModel
            {
                Id = e.Id,
                ImagePath = e.ImagePath,
                Title = e.Title,
                Synopsis = e.Synopsis,
                EpisodeNumber = e.EpisodeNumber,
                ReleaseDate = e.ReleaseDate,
                LengthMinutes = e.LengthMinutes,
                Rating = e.Rating,
                Rewatchability = e.Rewatchability,
                WatchDate = e.WatchDate,
                Watched = e.Watched,
                FirstWatch = e.FirstWatch,
                SeasonId = e.SeasonId,
                CollectionId = e.CollectionId,
                SelectedGenreIds = e.Genres.Select(g => g.Id).ToList(),
                SelectedActorIds = e.Actors.Select(a => a.Id).ToList(),
                SelectedDirectorIds = e.Directors.Select(d => d.Id).ToList(),
                SelectedWriterIds = e.Writers.Select(w => w.Id).ToList()
            };
        }

        public async Task<Guid> CreateAsync(EpisodeViewModel vm)
        {
            var seasonExists = await _context.Seasons.AnyAsync(x => x.Id == vm.SeasonId);
            if (!seasonExists) return Guid.Empty;

            if (vm.CollectionId.HasValue)
            {
                var collectionExists = await _context.Collections.AnyAsync(x => x.Id == vm.CollectionId.Value);
                if (!collectionExists) return Guid.Empty;
            }
            var episode = new Episode
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Title = vm.Title,
                Synopsis = vm.Synopsis,
                EpisodeNumber = vm.EpisodeNumber,
                ReleaseDate = vm.ReleaseDate,
                LengthMinutes = vm.LengthMinutes,
                Rating = vm.Rating,
                Rewatchability = vm.Rewatchability,
                WatchDate = vm.WatchDate,
                Watched = vm.Watched,
                FirstWatch = vm.FirstWatch,
                SeasonId = vm.SeasonId,
                CollectionId = vm.CollectionId
            };

            episode.Genres = await _context.Genres.Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToListAsync();
            episode.Actors = await _context.Actors.Where(x => vm.SelectedActorIds.Contains(x.Id)).ToListAsync();
            episode.Directors = await _context.Directors.Where(x => vm.SelectedDirectorIds.Contains(x.Id)).ToListAsync();
            episode.Writers = await _context.Writers.Where(x => vm.SelectedWriterIds.Contains(x.Id)).ToListAsync();

            _context.Episodes.Add(episode);
            await _context.SaveChangesAsync();
            return episode.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, EpisodeViewModel vm)
        {
            var seasonExists = await _context.Seasons.AnyAsync(x => x.Id == vm.SeasonId);
            if (!seasonExists) return false;

            if (vm.CollectionId.HasValue)
            {
                var collectionExists = await _context.Collections.AnyAsync(x => x.Id == vm.CollectionId.Value);
                if (!collectionExists) return false;
            }
            var episode = await _context.Episodes
                .Include(x => x.Genres)
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Include(x => x.Writers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (episode == null) return false;

            episode.ImagePath = vm.ImagePath;
            episode.Title = vm.Title;
            episode.Synopsis = vm.Synopsis;
            episode.EpisodeNumber = vm.EpisodeNumber;
            episode.ReleaseDate = vm.ReleaseDate;
            episode.LengthMinutes = vm.LengthMinutes;
            episode.Rating = vm.Rating;
            episode.Rewatchability = vm.Rewatchability;
            episode.WatchDate = vm.WatchDate;
            episode.Watched = vm.Watched;
            episode.FirstWatch = vm.FirstWatch;
            episode.SeasonId = vm.SeasonId;
            episode.CollectionId = vm.CollectionId;

            episode.Genres = await _context.Genres.Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToListAsync();
            episode.Actors = await _context.Actors.Where(x => vm.SelectedActorIds.Contains(x.Id)).ToListAsync();
            episode.Directors = await _context.Directors.Where(x => vm.SelectedDirectorIds.Contains(x.Id)).ToListAsync();
            episode.Writers = await _context.Writers.Where(x => vm.SelectedWriterIds.Contains(x.Id)).ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var episode = await _context.Episodes.FirstOrDefaultAsync(x => x.Id == id);
            if (episode == null) return false;

            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
            return true;
        }

        // dropdown helpers
        public async Task<List<SelectListItem>> GetSeasonSelectListAsync()
            => await _context.Seasons
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title })
                .ToListAsync();
    }
}