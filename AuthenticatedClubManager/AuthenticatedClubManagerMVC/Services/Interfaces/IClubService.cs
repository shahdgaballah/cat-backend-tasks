using AuthenticatedClubManagerMVC.Models;

namespace AuthenticatedClubManagerMVC.Services.Interfaces
{
    public interface IClubService
    {
        Task<List<Club>> GetAllAsync();
        Task<Club?> GetByIdAsync(int id);
        Task AddAsync(Club club);
        Task UpdateAsync(Club club);
        Task DeleteAsync(Club club);

        Task<bool> IsNameExistAsync(string name);
    }
}
