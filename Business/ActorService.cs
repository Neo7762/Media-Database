using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class ActorService
    {
        private readonly MediaContext _context;

        public ActorService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<ActorViewModel>> GetAllAsync()
        {
            var actors = await _context.Actors
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .ToListAsync();

            return actors.Select(a => new ActorViewModel
            {
                Id = a.Id,
                ImagePath = a.ImagePath,
                Name = a.Name,
                SelectedMovieIds = a.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = a.Episodes.Select(e => e.Id).ToList()
            }).ToList();
        }

        public async Task<ActorViewModel?> GetForEditAsync(Guid id)
        {
            var actor = await _context.Actors
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null) return null;

            return new ActorViewModel
            {
                Id = actor.Id,
                ImagePath = actor.ImagePath,
                Name = actor.Name,
                SelectedMovieIds = actor.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = actor.Episodes.Select(e => e.Id).ToList()
            };
        }

        public async Task<Guid> CreateAsync(ActorViewModel vm)
        {
            var actor = new Actor
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Name = vm.Name,
            };

            actor.Movies = await _context.Movies
                .Where(m => vm.SelectedMovieIds.Contains(m.Id))
                .ToListAsync();

            actor.Episodes = await _context.Episodes
                .Where(e => vm.SelectedEpisodeIds.Contains(e.Id))
                .ToListAsync();

            _context.Actors.Add(actor);
            await _context.SaveChangesAsync();
            return actor.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, ActorViewModel vm)
        {
            var actor = await _context.Actors
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null) return false;

            actor.ImagePath = vm.ImagePath;
            actor.Name = vm.Name;

            actor.Movies = await _context.Movies
                .Where(m => vm.SelectedMovieIds.Contains(m.Id))
                .ToListAsync();

            actor.Episodes = await _context.Episodes
                .Where(e => vm.SelectedEpisodeIds.Contains(e.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var actor = await _context.Actors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null) return false;

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return true;
        }

        // Dropdown helpers
        public async Task<List<SelectListItem>> GetMovieSelectListAsync()
            => await _context.Movies
                .OrderBy(m => m.Title)
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Title
                })
                .ToListAsync();

        public async Task<List<SelectListItem>> GetEpisodeSelectListAsync()
            => await _context.Episodes
                .OrderBy(e => e.Title)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Title
                })
                .ToListAsync();
    }
}