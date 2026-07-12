using System.IO;
using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class DirectorService
    {
        private readonly MediaContext _context;

        public DirectorService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<DirectorViewModel>> GetAllAsync()
        {
            var directors = await _context.Directors
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .ToListAsync();

            return directors.Select(a => new DirectorViewModel
            {
                Id = a.Id,
                ImagePath = a.ImagePath,
                Name = a.Name,
                SelectedMovieIds = a.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = a.Episodes.Select(e => e.Id).ToList()
            }).ToList();
        }

        public async Task<DirectorViewModel?> GetForEditAsync(Guid id)
        {
            var director = await _context.Directors
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (director == null) return null;

            return new DirectorViewModel
            {
                Id = director.Id,
                ImagePath = director.ImagePath,
                Name = director.Name,
                SelectedMovieIds = director.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = director.Episodes.Select(e => e.Id).ToList()
            };
        }

        public async Task<Guid> CreateAsync(DirectorViewModel vm)
        {
            var director = new Director
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Name = vm.Name,
            };

            director.Movies = await _context.Movies
                .Where(m => vm.SelectedMovieIds.Contains(m.Id))
                .ToListAsync();

            director.Episodes = await _context.Episodes
                .Where(e => vm.SelectedEpisodeIds.Contains(e.Id))
                .ToListAsync();

            _context.Directors.Add(director);
            await _context.SaveChangesAsync();
            return director.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, DirectorViewModel vm)
        {
            var director = await _context.Directors
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (director == null) return false;

            director.ImagePath = vm.ImagePath;
            director.Name = vm.Name;

            director.Movies = await _context.Movies
                .Where(m => vm.SelectedMovieIds.Contains(m.Id))
                .ToListAsync();

            director.Episodes = await _context.Episodes
                .Where(e => vm.SelectedEpisodeIds.Contains(e.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var director = await _context.Directors
                .FirstOrDefaultAsync(a => a.Id == id);

            if (director == null) return false;

            _context.Directors.Remove(director);
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