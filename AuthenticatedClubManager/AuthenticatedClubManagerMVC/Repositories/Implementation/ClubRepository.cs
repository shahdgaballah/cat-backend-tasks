using AuthenticatedClubManagerMVC.Data;
using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.Repositories.Interfaces;
using AuthenticatedClubManagerMVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AuthenticatedClubManagerMVC.Repositories.Implementation
{
    public class ClubRepository : IClubRepository
    {
        #region fields
        
        private readonly ApplicationDbContext _db;

       
        #endregion

        #region ctor
        public ClubRepository(ApplicationDbContext db)
        {
            _db = db;
            
        }
        #endregion

        #region methods

        //get all clubs
        public IQueryable<Club> GetAll()
        {
            return _db.Clubs.AsQueryable();
        }

        //get all clubs async
        public async Task<List<Club>> GetAllAsync()
        {
            return await _db.Clubs.ToListAsync();
        }

        //get a club by id
        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _db.Clubs.FirstOrDefaultAsync(c => c.Id == id);
        }

        //add club
        public async Task AddAsync(Club club)
        {
            await _db.Clubs.AddAsync(club);
            await _db.SaveChangesAsync();
            
        }
        //update club
        public async Task UpdateAsync(Club club)
        {
            _db.Clubs.Update(club);
            await _db.SaveChangesAsync();
        }

        //delete club
        public async Task DeleteAsync(Club club)
        {
            _db.Clubs.Remove(club);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            return await _db.Clubs.AnyAsync(c => c.Name == name);
        }
        #endregion
    }
}
