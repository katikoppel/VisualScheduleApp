namespace VisualScheduleApp.Core.Dto
{
    public class FileToApiDto
    {
        public Guid Id { get; set; }
        public string? FilePath { get; set; }
        public Guid? ActivityId { get; set; }
    }
}