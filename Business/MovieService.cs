using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class MovieService
    {
        private readonly MediaContext _context;

        public MovieService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<MovieViewModel>> GetAllAsync()
        {
            var movies = await _context.Movies
                .Include(m => m.Genres)
                .Include(m => m.Actors)
                .Include(m => m.Directors)
                .Include(m => m.Writers)
                .ToListAsync();

            return movies.Select(m => new MovieViewModel
            {
                Id = m.Id,
                ImagePath = m.ImagePath,
                Title = m.Title,
                Synopsis = m.Synopsis,
                ReleaseYear = m.ReleaseYear,
                LengthMinutes = m.LengthMinutes,
                CountryOfOrigin = m.CountryOfOrigin,
                Rating = m.Rating,
                Rewatchability = m.Rewatchability,
                WatchDate = m.WatchDate,
                Watched = m.Watched,
                FirstWatch = m.FirstWatch,
                SelectedGenreIds = m.Genres.Select(g => g.Id).ToList(),
                SelectedActorIds = m.Actors.Select(a => a.Id).ToList(),
                SelectedDirectorIds = m.Directors.Select(d => d.Id).ToList(),
                SelectedWriterIds = m.Writers.Select(w => w.Id).ToList()
            }).ToList();
        }

        public async Task<MovieViewModel?> GetForEditAsync(Guid id)
        {
            var m = await _context.Movies
                .Include(x => x.Genres)
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Include(x => x.Writers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (m == null) return null;

            return new MovieViewModel
            {
                Id = m.Id,
                ImagePath = m.ImagePath,
                Title = m.Title,
                Synopsis = m.Synopsis,
                ReleaseYear = m.ReleaseYear,
                LengthMinutes = m.LengthMinutes,
                CountryOfOrigin = m.CountryOfOrigin,
                Rating = m.Rating,
                Rewatchability = m.Rewatchability,
                WatchDate = m.WatchDate,
                Watched = m.Watched,
                FirstWatch = m.FirstWatch,
                SelectedGenreIds = m.Genres.Select(g => g.Id).ToList(),
                SelectedActorIds = m.Actors.Select(a => a.Id).ToList(),
                SelectedDirectorIds = m.Directors.Select(d => d.Id).ToList(),
                SelectedWriterIds = m.Writers.Select(w => w.Id).ToList()
            };
        }

        public async Task<Guid> CreateAsync(MovieViewModel vm)
        {
            var movie = new Movie
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Title = vm.Title,
                Synopsis = vm.Synopsis,
                ReleaseYear = vm.ReleaseYear,
                LengthMinutes = vm.LengthMinutes,
                CountryOfOrigin = vm.CountryOfOrigin,
                Rating = vm.Rating,
                Rewatchability = vm.Rewatchability,
                WatchDate = vm.WatchDate,
                Watched = vm.Watched,
                FirstWatch = vm.FirstWatch
            };

            movie.Genres = await _context.Genres.Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToListAsync();
            movie.Actors = await _context.Actors.Where(x => vm.SelectedActorIds.Contains(x.Id)).ToListAsync();
            movie.Directors = await _context.Directors.Where(x => vm.SelectedDirectorIds.Contains(x.Id)).ToListAsync();
            movie.Writers = await _context.Writers.Where(x => vm.SelectedWriterIds.Contains(x.Id)).ToListAsync();

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return movie.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, MovieViewModel vm)
        {
            var movie = await _context.Movies
                .Include(x => x.Genres)
                .Include(x => x.Actors)
                .Include(x => x.Directors)
                .Include(x => x.Writers)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null) return false;

            movie.ImagePath = vm.ImagePath;
            movie.Title = vm.Title;
            movie.Synopsis = vm.Synopsis;
            movie.ReleaseYear = vm.ReleaseYear;
            movie.LengthMinutes = vm.LengthMinutes;
            movie.CountryOfOrigin = vm.CountryOfOrigin;
            movie.Rating = vm.Rating;
            movie.Rewatchability = vm.Rewatchability;
            movie.WatchDate = vm.WatchDate;
            movie.Watched = vm.Watched;
            movie.FirstWatch = vm.FirstWatch;

            movie.Genres = await _context.Genres.Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToListAsync();
            movie.Actors = await _context.Actors.Where(x => vm.SelectedActorIds.Contains(x.Id)).ToListAsync();
            movie.Directors = await _context.Directors.Where(x => vm.SelectedDirectorIds.Contains(x.Id)).ToListAsync();
            movie.Writers = await _context.Writers.Where(x => vm.SelectedWriterIds.Contains(x.Id)).ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
            if (movie == null) return false;

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        // Dropdown helpers
        public async Task<List<SelectListItem>> GetGenreSelectListAsync()
            => await _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetActorSelectListAsync()
            => await _context.Actors
                .OrderBy(a => a.Name)
                .Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetDirectorSelectListAsync()
            => await _context.Directors
                .OrderBy(d => d.Name)
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetWriterSelectListAsync()
            => await _context.Writers
                .OrderBy(w => w.Name)
                .Select(w => new SelectListItem { Value = w.Id.ToString(), Text = w.Name })
                .ToListAsync();
    }
}