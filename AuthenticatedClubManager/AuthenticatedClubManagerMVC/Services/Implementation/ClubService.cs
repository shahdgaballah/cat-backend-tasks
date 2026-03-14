using AuthenticatedClubManagerMVC.Models;
using AuthenticatedClubManagerMVC.Repositories.Interfaces;
using AuthenticatedClubManagerMVC.Services.Interfaces;

namespace AuthenticatedClubManagerMVC.Services.Implementation
{
    public class ClubService : IClubService
    {

        private readonly IClubRepository _repo;

        private readonly IFileService _fileService;

        public ClubService(IClubRepository repo, IFileService fileService)
        {
            _repo = repo;
            _fileService = fileService;
        }

        public async Task<List<Club>> GetAllAsync()
        {
           return await  _repo.GetAllAsync();
        }


        public async Task<Club?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);

        }
        public async Task AddAsync(Club club)
        {
            await _repo.AddAsync(club);
            
        }

        public async Task UpdateAsync(Club club)
        {
            var currentPath = club.ImagePath;
            if (club.File != null || club.File?.Length > 0)
            {
                //delete old file
                _fileService.DeleteFile(currentPath);

                //upload new image
                var newPath = _fileService.UploadFile(club.File, "/images/");
                currentPath = newPath;

            }
            club.ImagePath = currentPath;

            await _repo.UpdateAsync(club);

        }

        public async Task DeleteAsync(Club club)
        {
            string path = club.ImagePath;

            await _repo.DeleteAsync(club);

            if (!string.IsNullOrEmpty(path))
            {
                _fileService.DeleteFile(path);
            }
        }

        public async Task<bool> IsNameExistAsync(string name)
        {
            return await _repo.IsNameExistAsync(name);
        }

    }
}
