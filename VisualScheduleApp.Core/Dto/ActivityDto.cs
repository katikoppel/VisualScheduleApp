using Microsoft.AspNetCore.Http;

namespace VisualScheduleApp.Core.Dto
{
    public class ActivityDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<IFormFile>? Files { get; set; }
        public IEnumerable<FileToApiDto> FileToApiDtos { get; set; } = new List<FileToApiDto>();
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}