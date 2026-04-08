namespace VisualScheduleApp.Core.Dto
{
    public class ScheduleItemDto
    {
        public Guid Id { get; set; }
        public int OrderIndex { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public Guid ScheduleId { get; set; }
        public Guid ActivityId { get; set; }

        public string? ActivityName { get; set; }
        public string? ActivityDescription { get; set; }
        public string? ActivityImagePath { get; set; }
    }
}
