namespace ProductManagementSystemMVC.Services.Interfaces
{
    public interface IFileService
    {
        string UploadFile(IFormFile file, string path);
    }
}
