using AuthenticatedClubManagerMVC.Services.Interfaces;

namespace AuthenticatedClubManagerMVC.Services.Implementation
{
    public class FileService : IFileService
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        

        public string UploadFile(IFormFile file, string path)
        {
            
                var filePath = _webHostEnvironment.WebRootPath + path;
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                using(FileStream fileStream=File.Create(filePath + fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                return $"{path}{fileName}";
        }

        public bool DeleteFile(string filePath)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot" + filePath);
            if (File.Exists(directoryPath))
            {
                File.Delete(directoryPath);
                return true;
            }
            return false;
        }
    }
}
