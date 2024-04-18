using Chinook.Models;

namespace Chinook.Contracts
{
    public interface IPlaylistService
    {
        string AddTrackToFavorites(string userId, long trackId);
        string RemoveTrackFromFavorites(string userId, long trackId);
    }
}
