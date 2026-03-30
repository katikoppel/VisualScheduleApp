namespace VisualScheduleApp.Core.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; } = null!;

        public Guid? ActivityId { get; set; }
        public virtual Activity? Activity { get; set; }
    }
}