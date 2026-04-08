using VisualScheduleApp.Core.Domain;

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
    }
}
