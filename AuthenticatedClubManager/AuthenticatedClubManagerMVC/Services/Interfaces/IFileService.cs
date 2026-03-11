namespace AuthenticatedClubManagerMVC.Services.Interfaces
{
    public interface IFileService
    {
        string UploadFile(IFormFile file, string path);

        bool DeleteFile(string filePath);
    }
}
