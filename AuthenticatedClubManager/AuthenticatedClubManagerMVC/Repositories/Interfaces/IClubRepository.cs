using AuthenticatedClubManagerMVC.Models;

namespace AuthenticatedClubManagerMVC.Repositories.Interfaces
{
    public interface IClubRepository
    {
        public IQueryable<Club> GetAll();
        public Task<List<Club>> GetAllAsync();
        public Task<Club?> GetByIdAsync(int id);
        public Task AddAsync(Club club);
        public Task UpdateAsync(Club club);
        public Task DeleteAsync(Club club);

        Task<bool> IsNameExistAsync(string name);
    }
}
