namespace VisualScheduleApp.Models.Children
{
    public class ChildViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int Age => DateTime.Today.Year - BirthDate.Year -
        (BirthDate.Date > DateTime.Today.AddYears(-(DateTime.Today.Year - BirthDate.Year)) ? 1 : 0);
    }
}
