using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class CollectionService
    {
        private readonly MediaContext _context;

        public CollectionService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<CollectionViewModel>> GetAllAsync()
        {
            var collections = await _context.Collections
                .Include(c => c.Movies)
                .Include(c => c.Episodes)
                .Include(c => c.Seasons)
                .Include(c => c.Shows)
                .ToListAsync();

            return collections.Select(c => new CollectionViewModel
            {
                Id = c.Id,
                ImagePath = c.ImagePath,
                Name = c.Name,
                Description = c.Description,
                SelectedMovieIds = c.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = c.Episodes.Select(e => e.Id).ToList(),
                SelectedSeasonIds = c.Seasons.Select(s => s.Id).ToList(),
                SelectedShowIds = c.Shows.Select(s => s.Id).ToList()
            }).ToList();
        }

        public async Task<CollectionViewModel?> GetForEditAsync(Guid id)
        {
            var c = await _context.Collections
                .Include(x => x.Movies)
                .Include(x => x.Episodes)
                .Include(x => x.Seasons)
                .Include(x => x.Shows)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (c == null) return null;

            return new CollectionViewModel
            {
                Id = c.Id,
                ImagePath = c.ImagePath,
                Name = c.Name,
                Description = c.Description,
                SelectedMovieIds = c.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = c.Episodes.Select(e => e.Id).ToList(),
                SelectedSeasonIds = c.Seasons.Select(s => s.Id).ToList(),
                SelectedShowIds = c.Shows.Select(s => s.Id).ToList()
            };
        }

        public async Task<Guid> CreateAsync(CollectionViewModel vm)
        {
            var collection = new Collection
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Name = vm.Name,
                Description = vm.Description
            };

            collection.Movies = await _context.Movies
                .Where(x => vm.SelectedMovieIds.Contains(x.Id))
                .ToListAsync();

            collection.Episodes = await _context.Episodes
                .Where(x => vm.SelectedEpisodeIds.Contains(x.Id))
                .ToListAsync();

            collection.Seasons = await _context.Seasons
                .Where(x => vm.SelectedSeasonIds.Contains(x.Id))
                .ToListAsync();

            collection.Shows = await _context.Shows
                .Where(x => vm.SelectedShowIds.Contains(x.Id))
                .ToListAsync();

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();
            return collection.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, CollectionViewModel vm)
        {
            var collection = await _context.Collections
                .Include(x => x.Movies)
                .Include(x => x.Episodes)
                .Include(x => x.Seasons)
                .Include(x => x.Shows)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (collection == null) return false;

            collection.ImagePath = vm.ImagePath;
            collection.Name = vm.Name;
            collection.Description = vm.Description;

            collection.Movies = await _context.Movies
                .Where(x => vm.SelectedMovieIds.Contains(x.Id))
                .ToListAsync();

            collection.Episodes = await _context.Episodes
                .Where(x => vm.SelectedEpisodeIds.Contains(x.Id))
                .ToListAsync();

            collection.Seasons = await _context.Seasons
                .Where(x => vm.SelectedSeasonIds.Contains(x.Id))
                .ToListAsync();

            collection.Shows = await _context.Shows
                .Where(x => vm.SelectedShowIds.Contains(x.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var collection = await _context.Collections.FirstOrDefaultAsync(x => x.Id == id);
            if (collection == null) return false;

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();
            return true;
        }

        // Dropdown helpers
        public async Task<List<SelectListItem>> GetMovieSelectListAsync()
            => await _context.Movies
                .OrderBy(m => m.Title)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Title })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetEpisodeSelectListAsync()
            => await _context.Episodes
                .OrderBy(e => e.Title)
                .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Title })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetSeasonSelectListAsync()
            => await _context.Seasons
                .OrderBy(s => s.Title)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetShowSelectListAsync()
            => await _context.Shows
                .OrderBy(s => s.Title)
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Title })
                .ToListAsync();
    }
}