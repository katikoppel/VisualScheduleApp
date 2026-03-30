
namespace VisualScheduleApp.Core.Domain
{
    public class Child
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        //public Guid UserId { get; set; }
    }
}
