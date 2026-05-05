using VisualScheduleApp.Core.Domain;
using VisualScheduleApp.Core.Dto;
using VisualScheduleApp.Core.ServiceInterface;

namespace VisualScheduleApp.Tests
{
    public class ActivityServicesTests : TestBase
    {
        [Fact]
        public async Task Should_CreateActivity_WhenValidData()
        {
            // Arrange
            var dto = MockActivityDto();

            // Act
            var result = await Svc<IActivityServices>().CreateAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Activity>(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
        }

        [Fact]
        public async Task Should_ReturnActivity_WhenIdExists()
        {
            // Arrange
            var created = await Svc<IActivityServices>().CreateAsync(MockActivityDto());

            // Act
            var result = await Svc<IActivityServices>().DetailAsync(created.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal(created.Name, result.Name);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenActivityNotFound()
        {
            // Arrange
            var wrongId = Guid.NewGuid();

            // Act
            var result = await Svc<IActivityServices>().DetailAsync(wrongId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_UpdateActivity_WhenDataChanges()
        {
            // Arrange
            var created = await Svc<IActivityServices>().CreateAsync(MockActivityDto());
            var oldModifiedAt = created.ModifiedAt;

            var updateDto = new ActivityDto
            {
                Id = created.Id,
                Name = "Uus tegevus",
                Description = "Uus kirjeldus"
            };

            // Act
            var result = await Svc<IActivityServices>().UpdateAsync(updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(created.Id, result.Id);
            Assert.Equal("Uus tegevus", result.Name);
            Assert.Equal("Uus kirjeldus", result.Description);
            Assert.NotEqual(oldModifiedAt, result.ModifiedAt);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenUpdatingMissingActivity()
        {
            // Arrange
            var updateDto = new ActivityDto
            {
                Id = Guid.NewGuid(),
                Name = "Puuduv tegevus",
                Description = "Ei ole"
            };

            // Act
            var result = await Svc<IActivityServices>().UpdateAsync(updateDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_DeleteActivity_WhenExists()
        {
            // Arrange
            var created = await Svc<IActivityServices>().CreateAsync(MockActivityDto());

            // Act
            var deleted = await Svc<IActivityServices>().DeleteAsync(created.Id);
            var result = await Svc<IActivityServices>().DetailAsync(created.Id);

            // Assert
            Assert.NotNull(deleted);
            Assert.Equal(created.Id, deleted.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_ReturnNull_WhenDeletingMissingActivity()
        {
            // Arrange
            var wrongId = Guid.NewGuid();

            // Act
            var result = await Svc<IActivityServices>().DeleteAsync(wrongId);

            // Assert
            Assert.Null(result);
        }

        private ActivityDto MockActivityDto()
        {
            return new ActivityDto
            {
                Name = "Söömine",
                Description = "Lõunasöögi söömine"
            };
        }
    }
}
