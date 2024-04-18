using Chinook.Contracts;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class ArtistService(IDbContextFactory<ChinookContext> dbFactory) : IArtistService
    {
        private readonly ChinookContext _context = dbFactory.CreateDbContext();

        public Artist GetArtistById(long artistId)
        {
            return _context.Artists.SingleOrDefault(a => a.ArtistId == artistId);
        }

        public async Task<List<Artist>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<List<Artist>> SearchArtistsByName(string searchKey)
        {
            return await _context.Artists
                .Where(a => a.Name.ToLower().Contains(searchKey.ToLower()))
                .ToListAsync();
        }
    }
}
