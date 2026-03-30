
namespace VisualScheduleApp.Core.Dto
{
    public class ChildDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        // public Guid UserId { get; set; }
    }
}
