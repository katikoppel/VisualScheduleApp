using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Domain;

namespace VisualScheduleApp.Data
{
    public class VisualScheduleAppContext : DbContext
    {
        public VisualScheduleAppContext(DbContextOptions<VisualScheduleAppContext> options)
            : base(options)
        {
        }

        public DbSet<Child> Children { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<FileToApi> FileToApis { get; set; }
    }
}
