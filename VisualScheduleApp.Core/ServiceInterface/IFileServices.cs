using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IFileServices
    {
        void FilesToApi(ActivityDto dto, Activity activity);
        Task<FileToApi?> RemoveImageFromApi(FileToApiDto dto);
        Task RemoveImagesFromApi(FileToApiDto[] dtos);
    }
}