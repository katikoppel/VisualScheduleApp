using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;

namespace VisualScheduleApp.ApplicationServices.Services
{
    public class ScheduleItemServices : IScheduleItemServices
    {
        private readonly VisualScheduleAppContext _context;

        public ScheduleItemServices(VisualScheduleAppContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduleItemDto>> GetAllByScheduleIdAsync(Guid scheduleId)
        {
            return await _context.ScheduleItems
                .Include(x => x.Activity)
                .Where(x => x.ScheduleId == scheduleId)
                .OrderBy(x => x.OrderIndex)
                .Select(x => new ScheduleItemDto
                {
                    Id = x.Id,
                    ScheduleId = x.ScheduleId,
                    ActivityId = x.ActivityId,
                    OrderIndex = x.OrderIndex,
                    Time = x.Time,
                    IsCompleted = x.IsCompleted,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                    ActivityName = x.Activity != null ? x.Activity.Name : null,
                    ActivityDescription = x.Activity != null ? x.Activity.Description : null,
                    ActivityImagePath = x.Activity != null ? x.Activity.ImagePath : null
                })
                .ToListAsync();
        }

        public async Task<ScheduleItemDto?> GetByIdAsync(Guid id)
        {
            var item = await _context.ScheduleItems
                .Include(x => x.Activity)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return null;
            }

            return new ScheduleItemDto
            {
                Id = item.Id,
                ScheduleId = item.ScheduleId,
                ActivityId = item.ActivityId,
                OrderIndex = item.OrderIndex,
                Time = item.Time,
                IsCompleted = item.IsCompleted,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt,
                ActivityName = item.Activity != null ? item.Activity.Name : null,
                ActivityDescription = item.Activity != null ? item.Activity.Description : null,
                ActivityImagePath = item.Activity != null ? item.Activity.ImagePath : null
            };
        }

        public async Task<ScheduleItemDto> CreateAsync(ScheduleItemDto dto)
        {
            var item = new ScheduleItem
            {
                Id = Guid.NewGuid(),
                ScheduleId = dto.ScheduleId,
                ActivityId = dto.ActivityId,
                OrderIndex = dto.OrderIndex,
                Time = dto.Time,
                IsCompleted = dto.IsCompleted,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            _context.ScheduleItems.Add(item);
            await _context.SaveChangesAsync();

            dto.Id = item.Id;
            dto.CreatedAt = item.CreatedAt;
            dto.ModifiedAt = item.ModifiedAt;

            var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == item.ActivityId);
            if (activity != null)
            {
                dto.ActivityName = activity.Name;
                dto.ActivityDescription = activity.Description;
                dto.ActivityImagePath = activity.ImagePath;
            }

            return dto;
        }

        public async Task<ScheduleItemDto?> UpdateAsync(ScheduleItemDto dto)
        {
            var item = await _context.ScheduleItems.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (item == null)
            {
                return null;
            }

            item.ScheduleId = dto.ScheduleId;
            item.ActivityId = dto.ActivityId;
            item.OrderIndex = dto.OrderIndex;
            item.Time = dto.Time;
            item.IsCompleted = dto.IsCompleted;
            item.ModifiedAt = DateTime.Now;

            _context.ScheduleItems.Update(item);
            await _context.SaveChangesAsync();

            dto.CreatedAt = item.CreatedAt;
            dto.ModifiedAt = item.ModifiedAt;

            var activity = await _context.Activities.FirstOrDefaultAsync(x => x.Id == item.ActivityId);
            if (activity != null)
            {
                dto.ActivityName = activity.Name;
                dto.ActivityDescription = activity.Description;
                dto.ActivityImagePath = activity.ImagePath;
            }

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var item = await _context.ScheduleItems.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return false;
            }

            _context.ScheduleItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}