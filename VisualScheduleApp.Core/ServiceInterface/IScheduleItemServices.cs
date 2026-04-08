using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IScheduleItemServices
    {
        Task<List<ScheduleItemDto>> GetAllByScheduleIdAsync(Guid scheduleId);
        Task<ScheduleItemDto?> GetByIdAsync(Guid id);
        Task<ScheduleItemDto> CreateAsync(ScheduleItemDto dto);
        Task<ScheduleItemDto?> UpdateAsync(ScheduleItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
