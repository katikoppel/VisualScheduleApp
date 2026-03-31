using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;

namespace VisualScheduleApp.ApplicationServices.Services
{
    public class ActivityServices : IActivityServices
    {
        private readonly VisualScheduleAppContext _context;
        private readonly IFileServices _fileServices;

        public ActivityServices(
            VisualScheduleAppContext context,
            IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Activity?> Create(ActivityDto dto)
        {
            Activity activity = new Activity
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _fileServices.FilesToApi(dto, activity);

            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity?> Update(ActivityDto dto)
        {
            var activity = await _context.Activities
                .Include(x => x.FileToApis)
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (activity == null)
            {
                return null;
            }

            activity.Name = dto.Name;
            activity.Description = dto.Description;
            activity.ModifiedAt = DateTime.Now;

            _fileServices.FilesToApi(dto, activity);

            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity?> DetailAsync(Guid id)
        {
            var result = await _context.Activities
                .Include(x => x.FileToApis)
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Activity?> Delete(Guid id)
        {
            var result = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            var images = await _context.FileToApis
                .Where(x => x.ActivityId == id)
                .Select(y => new FileToApiDto
                {
                    Id = y.Id,
                    ActivityId = y.ActivityId,
                    FilePath = y.FilePath
                })
                .ToArrayAsync();

            await _fileServices.RemoveImagesFromApi(images);

            _context.Activities.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}