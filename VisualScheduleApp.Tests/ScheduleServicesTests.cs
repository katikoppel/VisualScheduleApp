using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;

namespace VisualScheduleApp.Tests
{
    public class ScheduleServicesTests : TestBase
    {
        [Fact]
        public async Task Should_CreateSchedule_WhenValidData()
        {
            // Arrange
            var child = await Svc<IChildServices>().CreateAsync(MockChildDto());

            var dto = new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Test päevaplaan",
                Date = DateTime.Today,
                UserId = "test-user"
            };

            // Act
            var result = await Svc<IScheduleServices>().CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(child.Id, result.ChildId);
        }

        [Fact]
        public async Task Should_ReturnSchedule_WhenIdExists()
        {
            // Arrange
            var child = await Svc<IChildServices>().CreateAsync(MockChildDto());

            var created = await Svc<IScheduleServices>().CreateAsync(new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Detail test",
                Date = DateTime.Today,
                UserId = "user"
            });

            // Act
            var result = await Svc<IScheduleServices>().GetByIdAsync(created.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal("Detail test", result.Name);
            Assert.Equal(child.Name, result.ChildName);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenScheduleNotFound()
        {
            // Act
            var result = await Svc<IScheduleServices>().GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateSchedule_WhenDataChanges()
        {
            // Arrange
            var child = await Svc<IChildServices>().CreateAsync(MockChildDto());

            var created = await Svc<IScheduleServices>().CreateAsync(new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Algne nimi",
                Date = DateTime.Today,
                UserId = "user"
            });

            var updateDto = new ScheduleDto
            {
                Id = created.Id,
                ChildId = child.Id,
                Name = "Uus nimi",
                Date = DateTime.Today.AddDays(1),
                UserId = "user"
            };

            // Act
            var result = await Svc<IScheduleServices>().UpdateAsync(updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal("Uus nimi", result.Name);
            Assert.NotEqual(created.Name, result.Name);
            Assert.NotEqual(created.ModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenUpdatingMissingSchedule()
        {
            // Arrange
            var dto = new ScheduleDto
            {
                Id = Guid.NewGuid(),
                Name = "Puuduv",
                Date = DateTime.Today
            };

            // Act
            var result = await Svc<IScheduleServices>().UpdateAsync(dto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_DeleteSchedule_WhenExists()
        {
            // Arrange
            var child = await Svc<IChildServices>().CreateAsync(MockChildDto());

            var created = await Svc<IScheduleServices>().CreateAsync(new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Delete test",
                Date = DateTime.Today,
                UserId = "user"
            });

            // Act
            var deleted = await Svc<IScheduleServices>().DeleteAsync(created.Id);
            var result = await Svc<IScheduleServices>().GetByIdAsync(created.Id);

            // Assert
            Assert.True(deleted);
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_ReturnFalse_WhenDeletingMissingSchedule()
        {
            // Act
            var result = await Svc<IScheduleServices>().DeleteAsync(Guid.NewGuid());

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Should_ReturnTodaySchedule_WhenExists()
        {
            // Arrange
            var child = await Svc<IChildServices>().CreateAsync(MockChildDto());

            await Svc<IScheduleServices>().CreateAsync(new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Tänane plaan",
                Date = DateTime.Today,
                UserId = "user"
            });

            // Act
            var result = await Svc<IScheduleServices>().GetTodayAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tänane plaan", result.Name);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenNoTodaySchedule()
        {
            // Act
            var result = await Svc<IScheduleServices>().GetTodayAsync();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_ReturnAllSchedules()
        {
            // Arrange
            var child = await Svc<IChildServices>().CreateAsync(MockChildDto());

            await Svc<IScheduleServices>().CreateAsync(new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Plaan 1",
                Date = DateTime.Today,
                UserId = "user"
            });

            await Svc<IScheduleServices>().CreateAsync(new ScheduleDto
            {
                ChildId = child.Id,
                Name = "Plaan 2",
                Date = DateTime.Today.AddDays(1),
                UserId = "user"
            });

            // Act
            var result = await Svc<IScheduleServices>().GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count >= 2);
        }

        private ChildDto MockChildDto()
        {
            return new ChildDto
            {
                Name = "Testlaps",
                BirthDate = new DateTime(2020, 1, 1),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
    }
}