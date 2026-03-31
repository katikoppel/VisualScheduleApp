using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IActivityServices
    {
        Task<Activity> Create(ActivityDto dto);
        Task<Activity> Update(ActivityDto dto);
        Task<Activity> DetailAsync(Guid id);
        Task<Activity> Delete(Guid id);
    }
}
