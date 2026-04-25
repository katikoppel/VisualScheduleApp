using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;

namespace VisualScheduleApp.ApplicationServices.Services
{
    public class ScheduleServices : IScheduleServices
    {
        private readonly VisualScheduleAppContext _context;

        public ScheduleServices(VisualScheduleAppContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduleDto>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(x => x.Child)
                .Select(x => new ScheduleDto
                {
                    Id = x.Id,
                    ChildId = x.ChildId,
                    ChildName = x.Child != null ? x.Child.Name : null,
                    Date = x.Date,
                    Name = x.Name,
                    CreatedAt = x.CreatedAt,
                    ModifiedAt = x.ModifiedAt,
                    UserId = x.UserId
                })
                .ToListAsync();
        }

        public async Task<ScheduleDto?> GetByIdAsync(Guid id)
        {
            var schedule = await _context.Schedules
                .Include(x => x.Child)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
            {
                return null;
            }

            return new ScheduleDto
            {
                Id = schedule.Id,
                ChildId = schedule.ChildId,
                ChildName = schedule.Child?.Name,
                Date = schedule.Date,
                Name = schedule.Name,
                CreatedAt = schedule.CreatedAt,
                ModifiedAt = schedule.ModifiedAt,
                UserId = schedule.UserId
            };
        }

        public async Task<ScheduleDto> CreateAsync(ScheduleDto dto)
        {
            var schedule = new Schedule
            {
                Id = Guid.NewGuid(),
                ChildId = dto.ChildId,
                Date = dto.Date,
                Name = dto.Name,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                UserId = dto.UserId
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            dto.Id = schedule.Id;
            dto.CreatedAt = schedule.CreatedAt;
            dto.ModifiedAt = schedule.ModifiedAt;
            dto.UserId = schedule.UserId;

            return dto;
        }

        public async Task<ScheduleDto?> UpdateAsync(ScheduleDto dto)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (schedule == null)
            {
                return null;
            }

            schedule.ChildId = dto.ChildId;
            schedule.Date = dto.Date;
            schedule.Name = dto.Name;
            schedule.ModifiedAt = DateTime.Now;
            schedule.UserId = dto.UserId;

            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();

            dto.CreatedAt = schedule.CreatedAt;
            dto.ModifiedAt = schedule.ModifiedAt;
            dto.UserId = schedule.UserId;

            return dto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(x => x.Id == id);

            if (schedule == null)
            {
                return false;
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ScheduleDto?> GetTodayAsync()
        {
            var today = DateTime.Today;

            var schedule = await _context.Schedules
                .Include(x => x.Child)
                .FirstOrDefaultAsync(x => x.Date.Date == today);

            if (schedule == null)
            {
                return null;
            }

            return new ScheduleDto
            {
                Id = schedule.Id,
                ChildId = schedule.ChildId,
                ChildName = schedule.Child != null ? schedule.Child.Name : null,
                Date = schedule.Date,
                Name = schedule.Name,
                CreatedAt = schedule.CreatedAt,
                ModifiedAt = schedule.ModifiedAt,
                UserId = schedule.UserId
            };
        }
    }
}