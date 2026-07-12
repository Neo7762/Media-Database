using System.IO;
using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class WriterService
    {
        private readonly MediaContext _context;

        public WriterService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<WriterViewModel>> GetAllAsync()
        {
            var writers = await _context.Writers
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .ToListAsync();

            return writers.Select(a => new WriterViewModel
            {
                Id = a.Id,
                ImagePath = a.ImagePath,
                Name = a.Name,
                SelectedMovieIds = a.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = a.Episodes.Select(e => e.Id).ToList()
            }).ToList();
        }

        public async Task<WriterViewModel?> GetForEditAsync(Guid id)
        {
            var writer = await _context.Writers
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (writer == null) return null;

            return new WriterViewModel
            {
                Id = writer.Id,
                ImagePath = writer.ImagePath,
                Name = writer.Name,
                SelectedMovieIds = writer.Movies.Select(m => m.Id).ToList(),
                SelectedEpisodeIds = writer.Episodes.Select(e => e.Id).ToList()
            };
        }

        public async Task<Guid> CreateAsync(WriterViewModel vm)
        {
            var writer = new Writer
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Name = vm.Name,
            };

            writer.Movies = await _context.Movies
                .Where(m => vm.SelectedMovieIds.Contains(m.Id))
                .ToListAsync();

            writer.Episodes = await _context.Episodes
                .Where(e => vm.SelectedEpisodeIds.Contains(e.Id))
                .ToListAsync();

            _context.Writers.Add(writer);
            await _context.SaveChangesAsync();
            return writer.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, WriterViewModel vm)
        {
            var writer = await _context.Writers
                .Include(a => a.Movies)
                .Include(a => a.Episodes)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (writer == null) return false;

            writer.ImagePath = vm.ImagePath;
            writer.Name = vm.Name;

            writer.Movies = await _context.Movies
                .Where(m => vm.SelectedMovieIds.Contains(m.Id))
                .ToListAsync();

            writer.Episodes = await _context.Episodes
                .Where(e => vm.SelectedEpisodeIds.Contains(e.Id))
                .ToListAsync();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var writer = await _context.Writers
                .FirstOrDefaultAsync(a => a.Id == id);

            if (writer == null) return false;

            _context.Writers.Remove(writer);
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