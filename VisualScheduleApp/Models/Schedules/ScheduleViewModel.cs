using Microsoft.AspNetCore.Mvc.Rendering;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Models.ScheduleItems;

namespace VisualScheduleApp.Models.Schedules
{
    public class ScheduleViewModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid ChildId { get; set; }
        public string? ChildName { get; set; }

        public IEnumerable<SelectListItem>? Children { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public List<ScheduleItemViewModel>? ScheduleItems { get; set; }
    }
}
