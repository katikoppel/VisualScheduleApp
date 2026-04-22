using Microsoft.AspNetCore.Mvc.Rendering;
using VisualScheduleApp.Core.Domain;

namespace VisualScheduleApp.Models.ScheduleItems
{
    public class ScheduleItemViewModel
    {
        public Guid Id { get; set; }
        public int OrderIndex { get; set; }
        public TimeSpan? Time { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid ScheduleId { get; set; }

        public Guid ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public string? ActivityDescription { get; set; }
        public string? ActivityImagePath { get; set; }

        public IEnumerable<SelectListItem>? Activities { get; set; }
        public IEnumerable<SelectListItem>? Schedules { get; set; }
    }
}