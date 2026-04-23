using Microsoft.AspNetCore.Identity;

namespace VisualScheduleApp.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
