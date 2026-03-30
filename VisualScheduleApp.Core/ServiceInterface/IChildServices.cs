using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;

namespace VisualScheduleApp.Core.ServiceInterface
{
    public interface IChildServices
    {
        Task<Child> Create(ChildDto dto);
        Task<Child> Update(ChildDto dto);
        Task<Child> DetailAsync(Guid id);
        Task<Child> Delete(Guid id);
    }
}
