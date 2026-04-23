using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.ApplicationServices.Services;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;
using VisualScheduleApp.Models.Email;
using VisualScheduleApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<VisualScheduleAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IChildServices, ChildServices>();
builder.Services.AddScoped<IActivityServices, ActivityServices>();  
builder.Services.AddScoped<IFileServices, FileServices>();
builder.Services.AddScoped<IScheduleServices, ScheduleServices>();
builder.Services.AddScoped<IScheduleItemServices, ScheduleItemServices>();
builder.Services.AddTransient<IEmailSender, EmailServices>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<VisualScheduleAppContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();

app.Run();
