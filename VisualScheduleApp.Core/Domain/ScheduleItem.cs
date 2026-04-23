namespace VisualScheduleApp.Core.Domain
{
    public class ScheduleItem
    {
        public Guid Id { get; set; }
        public int OrderIndex { get; set; }
        public TimeSpan Time { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid ScheduleId { get; set; }
        public Schedule? Schedule { get; set; }

        public Guid ActivityId { get; set; }
        public Activity? Activity { get; set; }
    }
}