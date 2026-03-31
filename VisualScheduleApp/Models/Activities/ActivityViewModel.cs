using Microsoft.AspNetCore.Http;

namespace VisualScheduleApp.Models.Activities
{
    public class ActivityViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<IFormFile>? Files { get; set; }
        public List<ImageViewModel> Image { get; set; } = new();
    }
}