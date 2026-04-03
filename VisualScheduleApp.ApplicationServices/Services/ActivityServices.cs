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

        public ActivityServices
            (
            VisualScheduleAppContext context,
            IFileServices fileServices
            )
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

            // File upload handling
            if (dto.Files != null && dto.Files.Count > 0)
            {
                activity.ImagePath = await _fileServices.UploadFile(dto.Files[0]);
            }

            await _context.Activities.AddAsync(activity);
            await _context.SaveChangesAsync();

            return activity;
        }

        public async Task<Activity?> Update(ActivityDto dto)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (activity == null)
            {
                return null;
            }

            activity.Name = dto.Name;
            activity.Description = dto.Description;
            activity.ModifiedAt = DateTime.Now;

            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!string.IsNullOrEmpty(activity.ImagePath))
                {
                    _fileServices.DeleteFile(activity.ImagePath);
                }

                activity.ImagePath = await _fileServices.UploadFile(dto.Files[0]);
            }

            await _context.SaveChangesAsync();
            return activity;
        }

        public async Task<Activity?> DetailAsync(Guid id)
        {
            return await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Activity?> Delete(Guid id)
        {
            var activity = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == id);

            if (activity == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(activity.ImagePath))
            {
                _fileServices.DeleteFile(activity.ImagePath);
            }

            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();

            return activity;
        }
    }
}