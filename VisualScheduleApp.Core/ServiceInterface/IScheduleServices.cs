using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IScheduleServices
    {
        Task<List<ScheduleDto>> GetAllAsync();
        Task<ScheduleDto?> GetByIdAsync(Guid id);
        Task<ScheduleDto> CreateAsync(ScheduleDto dto);
        Task<ScheduleDto?> UpdateAsync(ScheduleDto dto);
        Task<bool> DeleteAsync(Guid id);

        Task<ScheduleDto?> GetTodayAsync();
    }
}
