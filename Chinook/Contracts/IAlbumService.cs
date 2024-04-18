using Chinook.Models;

namespace Chinook.Contracts
{
    public interface IAlbumService
    {
        Task<List<Album>> GetAlbumsByArtistId(int artistId);
    }
}
