using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using VisualScheduleApp.Data;

namespace VisualScheduleApp.ApplicationServices.Services
{
    public class ChildServices : IChildServices
    {
        private readonly VisualScheduleAppContext _context;

        public ChildServices(VisualScheduleAppContext context)
        {
            _context = context;
        }

        public async Task<Child> Create(ChildDto dto)
        {
            Child child = new Child();

            child.Id = Guid.NewGuid();
            child.Name = dto.Name;
            child.BirthDate = dto.BirthDate;
            child.CreatedAt = DateTime.Now;
            child.ModifiedAt = DateTime.Now;

            await _context.Children.AddAsync(child);
            await _context.SaveChangesAsync();

            return child;
        }

        public async Task<Child> Update(ChildDto dto)
        {
            Child child = new Child();

            child.Id = dto.Id;
            child.Name = dto.Name;
            child.BirthDate = dto.BirthDate;
            child.CreatedAt = dto.CreatedAt;
            child.ModifiedAt = DateTime.Now;

            _context.Children.Update(child);
            await _context.SaveChangesAsync();

            return child;
        }

        public async Task<Child> DetailAsync(Guid id)
        {
            var result = await _context.Children
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public async Task<Child> Delete(Guid id)
        {
            var result = await _context.Children
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return null;
            }

            _context.Children.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }
    }
}