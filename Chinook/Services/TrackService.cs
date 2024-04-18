using Chinook.ClientModels;
using Chinook.Contracts;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class TrackService(IDbContextFactory<ChinookContext> dbFactory) : ITrackService
    {
        private readonly ChinookContext _context = dbFactory.CreateDbContext();

        public List<PlaylistTrack> GetTracksByArtist(long artistId, string userId)
        {
            return _context.Tracks.Where(a => a.Album.ArtistId == artistId)
                .Include(a => a.Album)
                .Select(t => new PlaylistTrack()
                {
                    AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == userId && up.Playlist.Name == "My favorite tracks")).Any()
                })
                .ToList();
        }
    }
}
