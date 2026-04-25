using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IChildServices
    {
        Task<Child> CreateAsync(ChildDto dto);
        Task<Child> UpdateAsync(ChildDto dto);
        Task<Child> DetailAsync(Guid id);
        Task<Child> DeleteAsync(Guid id);
    }
}
