namespace VisualScheduleApp.Models.Activities
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public string? FilePath { get; set; }
        public Guid? ActivityId { get; set; }
    }
}