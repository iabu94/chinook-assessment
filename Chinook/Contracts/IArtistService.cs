using Chinook.Models;

namespace Chinook.Contracts
{
    public interface IArtistService
    {
        Task<List<Artist>> GetArtists();
        Task<List<Artist>> SearchArtistsByName(string searchKey);
        Artist GetArtistById(long artistId);
    }
}
