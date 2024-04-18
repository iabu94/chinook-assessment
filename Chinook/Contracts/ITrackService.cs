using Chinook.ClientModels;

namespace Chinook.Contracts
{
    public interface ITrackService
    {
        List<PlaylistTrack> GetTracksByArtist(long artistId, string userId);
    }
}
