using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class SeasonService
    {
        private readonly MediaContext _context;

        public SeasonService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<SeasonViewModel>> GetAllAsync()
        {
            var seasons = await _context.Seasons
                .Include(s => s.Show)
                .ToListAsync();

            return seasons.Select(s => new SeasonViewModel
            {
                Id = s.Id,
                ImagePath = s.ImagePath,
                Title = s.Title,
                Synopsis = s.Synopsis,
                SeasonNumber = s.SeasonNumber,
                ReleaseYear = s.ReleaseYear,
                ShowId = s.ShowId
            }).ToList();
        }

        public async Task<SeasonViewModel?> GetForEditAsync(Guid id)
        {
            var s = await _context.Seasons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (s == null) return null;

            return new SeasonViewModel
            {
                Id = s.Id,
                ImagePath = s.ImagePath,
                Title = s.Title,
                Synopsis = s.Synopsis,
                SeasonNumber = s.SeasonNumber,
                ReleaseYear = s.ReleaseYear,
                ShowId = s.ShowId
            };
        }

        public async Task<Guid> CreateAsync(SeasonViewModel vm)
        {
            var showExists = await _context.Shows.AnyAsync(x => x.Id == vm.ShowId);
            if (!showExists) return Guid.Empty;

            var season = new Season
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Title = vm.Title,
                Synopsis = vm.Synopsis,
                SeasonNumber = vm.SeasonNumber,
                ReleaseYear = vm.ReleaseYear,
                ShowId = vm.ShowId
            };

            _context.Seasons.Add(season);
            await _context.SaveChangesAsync();
            return season.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, SeasonViewModel vm)
        {
            var season = await _context.Seasons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (season == null) return false;

            var showExists = await _context.Shows.AnyAsync(x => x.Id == vm.ShowId);
            if (!showExists) return false;

            season.ImagePath = vm.ImagePath;
            season.Title = vm.Title;
            season.Synopsis = vm.Synopsis;
            season.SeasonNumber = vm.SeasonNumber;
            season.ReleaseYear = vm.ReleaseYear;
            season.ShowId = vm.ShowId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var season = await _context.Seasons.FirstOrDefaultAsync(x => x.Id == id);
            if (season == null) return false;

            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<SelectListItem>> GetShowSelectListAsync()
            => await _context.Shows
                .OrderBy(s => s.Title)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                })
                .ToListAsync();
    }
}