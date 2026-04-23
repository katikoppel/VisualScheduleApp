using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Domain;

namespace VisualScheduleApp.Data
{
    public class VisualScheduleAppContext : IdentityDbContext<ApplicationUser>
    {
        public VisualScheduleAppContext(DbContextOptions<VisualScheduleAppContext> options)
            : base(options)
        {
        }

        public DbSet<Child> Children { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ScheduleItem> ScheduleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ScheduleItem>()
                .HasIndex(x => new { x.ScheduleId, x.OrderIndex })
                .IsUnique();

            modelBuilder.Entity<ScheduleItem>()
                .HasIndex(x => new { x.ScheduleId, x.Time })
                .IsUnique();
        }
    }
}