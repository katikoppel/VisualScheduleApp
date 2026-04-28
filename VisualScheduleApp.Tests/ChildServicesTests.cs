using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using VisualScheduleApp.Data;

namespace VisualScheduleApp.Tests
{
    public class ChildServicesTests : TestBase
    {
        [Fact]
        public async Task Should_CreateChild_WhenValidData()
        {
            // Arrange
            var dto = MockChildDto();

            // Act
            var result = await Svc<IChildServices>().CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Child>(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task Should_ReturnChild_WhenIdExists()
        {
            // Arrange
            var created = await Svc<IChildServices>().CreateAsync(MockChildDto());

            // Act
            var result = await Svc<IChildServices>().DetailAsync(created.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal(created.Name, result.Name);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenChildNotFound()
        {
            // Arrange
            var wrongId = Guid.NewGuid();

            // Act
            var result = await Svc<IChildServices>().DetailAsync(wrongId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateChild_WhenDataChanges()
        {
            // Arrange
            var created = await Svc<IChildServices>().CreateAsync(MockChildDto());

            var db = Svc<VisualScheduleAppContext>();
            db.Entry(created).State = EntityState.Detached;

            var updateDto = new ChildDto
            {
                Id = created.Id,
                Name = "Uus nimi",
                BirthDate = new DateTime(2018, 1, 1),
                CreatedAt = created.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IChildServices>().UpdateAsync(updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal("Uus nimi", result.Name);
            Assert.NotEqual(created.Name, result.Name);
        }

        [Fact]
        public async Task Should_DeleteChild_WhenExists()
        {
            // Arrange
            var created = await Svc<IChildServices>().CreateAsync(MockChildDto());

            // Act
            var deleted = await Svc<IChildServices>().DeleteAsync(created.Id);
            var result = await Svc<IChildServices>().DetailAsync(created.Id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(created.Id, deleted.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenDeletingMissingChild()
        {
            // Arrange
            var wrongId = Guid.NewGuid();

            // Act
            var result = await Svc<IChildServices>().DeleteAsync(wrongId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_KeepCreatedAt_WhenUpdating()
        {
            // Arrange
            var created = await Svc<IChildServices>().CreateAsync(MockChildDto());

            var db = Svc<VisualScheduleAppContext>();
            db.Entry(created).State = EntityState.Detached;

            var updateDto = new ChildDto
            {
                Id = created.Id,
                Name = "Muudetud nimi",
                BirthDate = created.BirthDate,
                CreatedAt = created.CreatedAt,
                ModifiedAt = DateTime.UtcNow
            };

            // Act
            var result = await Svc<IChildServices>().UpdateAsync(updateDto);

            // Assert
            Assert.Equal(created.CreatedAt, result.CreatedAt);
            Assert.NotEqual(created.ModifiedAt, result.ModifiedAt);
        }

        private ChildDto MockChildDto()
        {
            return new ChildDto
            {
                Name = "Lapse nimi",
                BirthDate = new DateTime(2020, 1, 1),
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };
        }
    }
}