namespace VisualScheduleApp.Core.Domain
{
    public class Schedule
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid ChildId { get; set; }
        public Child? Child { get; set; }

        public ICollection<ScheduleItem> ScheduleItems { get; set; } = new List<ScheduleItem>();
    }
}