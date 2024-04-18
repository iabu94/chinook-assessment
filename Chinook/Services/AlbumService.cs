using Chinook.Contracts;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class AlbumService(IDbContextFactory<ChinookContext> dbFactory) : IAlbumService
    {
        private readonly ChinookContext _context = dbFactory.CreateDbContext();

        public async Task<List<Album>> GetAlbumsByArtistId(int artistId)
        {
            return await _context.Albums.Where(a => a.ArtistId == artistId).ToListAsync();
        }
    }
}
