namespace VisualScheduleApp.Core.Domain
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ICollection<FileToApi>? FileToApis { get; set; }
    }
}