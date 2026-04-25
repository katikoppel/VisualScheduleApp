using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IActivityServices
    {
        Task<Activity> CreateAsync(ActivityDto dto);
        Task<Activity?> UpdateAsync(ActivityDto dto);
        Task<Activity?> DetailAsync(Guid id);
        Task<Activity?> DeleteAsync(Guid id);
    }
}
