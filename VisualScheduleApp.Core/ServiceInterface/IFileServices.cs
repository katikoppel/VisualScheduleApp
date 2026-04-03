using Microsoft.AspNetCore.Http;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IFileServices
    {
        Task<string> UploadFile(IFormFile file);
        void DeleteFile(string filePath);
    }
}