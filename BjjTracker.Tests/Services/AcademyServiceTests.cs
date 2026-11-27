using BjjTracker.Models;
using BjjTracker.Repositories;
using BjjTracker.Services;
using Moq;
using Xunit;

namespace BjjTracker.Tests.Services
{
    public class AcademyServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_WhenAcademyExists_ReturnsAcademy()
        {
            // Arrange
            var academy = new Academy { Id = 1, AcademyName = "Test Club" };
            var mockRepository = new Mock<IRepository<Academy>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(academy);
            var service = new AcademyService(mockRepository.Object);
            // Act
            var result = await service.GetByIdAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
        [Fact]
        public async Task DeleteAsync_WhenAcademyExists_DeletesAndSaves()
        {
            // Arrange
            var academy = new Academy { Id = 1, AcademyName = "Test Club" };
            var mockRepository = new Mock<IRepository<Academy>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(academy);
            var service = new AcademyService(mockRepository.Object);
            // Act
            await service.DeleteAsync(1);
            // Assert
            mockRepository.Verify(r => r.Delete(academy), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_WhenAcademyMissing_DoesNothing()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Academy>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Academy?)null);
            var service = new AcademyService(mockRepository.Object);
            // Act
            await service.DeleteAsync(1);
            // Assert
            mockRepository.Verify(r => r.Delete(It.IsAny<Academy>()), Times.Never);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
        [Fact]
        public async Task CreateAsync_AddsAcademyAndSaves()
        {
            // Arrange
            var academy = new Academy { Id = 1, AcademyName = "Test Club" };
            var mockRepository = new Mock<IRepository<Academy>>();
            var service = new AcademyService(mockRepository.Object);
            // Act
            await service.CreateAsync(academy);
            // Assert
            mockRepository.Verify(r => r.AddAsync(academy), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_UpdatesAcademyAndSaves()
        {
            // Arrange
            var academy = new Academy { Id = 1, AcademyName = "Test Club" };
            var mockRepository = new Mock<IRepository<Academy>>();
            var service = new AcademyService(mockRepository.Object);
            // Act
            await service.UpdateAsync(academy);
            // Assert
            mockRepository.Verify(r => r.Update(academy), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task GetAllAsync_ReturnsAllAcademies()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();
            context.Academies.AddRange(
                new Academy { AcademyName = "First Testing BJJ Club" },
                new Academy { AcademyName = "Second Testing BJJ Club" });
            await context.SaveChangesAsync();
            var service = new AcademyService(new Repository<Academy>(context));
            // Act
            var result = await service.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}