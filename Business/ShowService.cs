using Media_Database.Data;
using Media_Database.Models;
using Media_Database.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Media_Database.Business
{
    public class ShowService
    {
        private readonly MediaContext _context;

        public ShowService(MediaContext context)
        {
            _context = context;
        }

        public async Task<List<ShowViewModel>> GetAllAsync()
        {
            var shows = await _context.Shows
                .ToListAsync();

            return shows.Select(s => new ShowViewModel
            {
                Id = s.Id,
                ImagePath = s.ImagePath,
                Title = s.Title,
                Synopsis = s.Synopsis,
                ReleaseYear = s.ReleaseYear,
                WatchStatus = s.WatchStatus
            }).ToList();
        }

        public async Task<ShowViewModel?> GetForEditAsync(Guid id)
        {
            var s = await _context.Shows
                .FirstOrDefaultAsync(x => x.Id == id);

            if (s == null) return null;

            return new ShowViewModel
            {
                Id = s.Id,
                ImagePath = s.ImagePath,
                Title = s.Title,
                Synopsis = s.Synopsis,
                ReleaseYear = s.ReleaseYear,
                WatchStatus = s.WatchStatus
            };
        }

        public async Task<Guid> CreateAsync(ShowViewModel vm)
        {
            var show = new Show
            {
                Id = Guid.NewGuid(),
                ImagePath = vm.ImagePath,
                Title = vm.Title,
                Synopsis = vm.Synopsis,
                ReleaseYear = vm.ReleaseYear,
                WatchStatus = vm.WatchStatus
            };

            _context.Shows.Add(show);
            await _context.SaveChangesAsync();
            return show.Id;
        }

        public async Task<bool> UpdateAsync(Guid id, ShowViewModel vm)
        {
            var show = await _context.Shows
                .FirstOrDefaultAsync(x => x.Id == id);

            if (show == null) return false;

            show.ImagePath = vm.ImagePath;
            show.Title = vm.Title;
            show.Synopsis = vm.Synopsis;
            show.ReleaseYear = vm.ReleaseYear;
            show.WatchStatus = vm.WatchStatus;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var show = await _context.Shows
                .FirstOrDefaultAsync(x => x.Id == id);

            if (show == null) return false;

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}