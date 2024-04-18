using Chinook.Contracts;
using Chinook.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    public class PlaylistService(IDbContextFactory<ChinookContext> dbFactory) : IPlaylistService
    {
        private readonly ChinookContext _context = dbFactory.CreateDbContext();

        public string AddTrackToFavorites(string userId, long trackId)
        {
            // Check whether the My favorite tracks exists
            var userPlaylist = _context.UserPlaylists
                .Where(u => u.UserId == userId && u.Playlist.Name == "My favorite tracks")
                .FirstOrDefault();

            if (userPlaylist == null)
            {
                // Add My favorite tracks if not exists
                _context.UserPlaylists.Add(new UserPlaylist
                {
                    UserId = userId,
                    Playlist = new Models.Playlist
                    {
                        Name = "My favorite tracks"
                    }
                }
                );
                _context.SaveChanges();
            }

            // Get the playlist tracked entity
            var playlist = _context.Playlists.Where(p => p.UserPlaylists.Any(u => u.UserId == userId && u.Playlist.Name == "My favorite tracks"))
                .FirstOrDefault();
            var track = _context.Tracks.FirstOrDefault(t => t.TrackId == trackId);

            if (playlist != null)
            {
                // find the track and add it to the playlist
                playlist.Tracks.Add(track);
                _context.SaveChanges();
                return $"Track {track.Name} added to playlist Favorites.";
            }
            return $"Track {track.Name} added to playlist Favorites.";
        }

        public string RemoveTrackFromFavorites(string userId, long trackId)
        {
            var playlist = _context.Playlists
                .Where(p => p.UserPlaylists.Any(u => u.UserId == userId && u.Playlist.Name == "My favorite tracks"))
                .Include(p => p.Tracks)
                .FirstOrDefault();
            var track = _context.Tracks.FirstOrDefault(t => t.TrackId == trackId);

            if (playlist != null)
            {
                playlist.Tracks.Remove(track);
                _context.SaveChanges();
                return $"Track {track.Name} removed from playlist Favorites.";
            }
            return $"Track {track.Name} not removed from playlist Favorites.";
        }
    }
}
