using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using VisualScheduleApp.Core.ServiceInterface;

namespace VisualScheduleApp.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _env;

        public FileServices(IHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(_env.ContentRootPath, "wwwroot", "images");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/images/" + uniqueFileName;
        }

        public void DeleteFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            var fullPath = Path.Combine(_env.ContentRootPath, "wwwroot", filePath.TrimStart('/'));

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}