using BjjTracker.Models;
using BjjTracker.Models.Enums;
using BjjTracker.Repositories;
using BjjTracker.Services;
using Moq;
using Xunit;
using BjjTracker.Data;
using Microsoft.EntityFrameworkCore;

namespace BjjTracker.Tests
{
    public class TechniqueServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_WhenTechniqueExists_ReturnsTechnique()
        {
            // Arrange
            var technique = new Technique
            {
                Id = 1,
                TechniqueName = "Armbar",
                TechniquePosition = TechniquePosition.ClosedGuard
            };
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(technique);
            var service = new TechniqueService(mockRepository.Object);
            // Act
            var result = await service.GetByIdAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Armbar", result.TechniqueName);
        }
        [Fact]
        public async Task DeleteAsync_WhenTechniqueExists_DeletesAndSaves()
        {
            // Arrange
            var technique = new Technique
            {
                Id = 1,
                TechniqueName = "Armbar",
                TechniquePosition = TechniquePosition.ClosedGuard
            };
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(technique);
            var service = new TechniqueService(mockRepository.Object);
            // Act
            await service.DeleteAsync(1);
            // Assert
            mockRepository.Verify(r => r.Delete(technique), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_WhenTechniqueMissing_DoesNothing()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<Technique>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Technique?)null);
            var service = new TechniqueService(mockRepository.Object);
            // Act
            await service.DeleteAsync(1);
            // Assert
            mockRepository.Verify(r => r.Delete(It.IsAny<Technique>()), Times.Never);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
        [Fact]
        public async Task CreateAsync_AddsTechniqueAndSaves()
        {
            // Arrange
            var technique = new Technique
            {
                Id = 1,
                TechniqueName = "Armbar",
                TechniquePosition = TechniquePosition.ClosedGuard
            };
            var mockRepository = new Mock<IRepository<Technique>>();
            var service = new TechniqueService(mockRepository.Object);
            // Act
            await service.CreateAsync(technique);
            // Assert
            mockRepository.Verify(r => r.AddAsync(technique), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_UpdatesTechniqueAndSaves()
        {
            // Arrange
            var technique = new Technique
            {
                Id = 1,
                TechniqueName = "Armbar",
                TechniquePosition = TechniquePosition.ClosedGuard
            };
            var mockRepository = new Mock<IRepository<Technique>>();
            var service = new TechniqueService(mockRepository.Object);
            // Act
            await service.UpdateAsync(technique);
            // Assert
            mockRepository.Verify(r => r.Update(technique), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        private static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }
        [Fact]
        public async Task GetAllAsync_ReturnsAllTechniques()
        {
            // Arrange
            using var context = CreateContext();
            context.Techniques.AddRange(
                new Technique { TechniqueName = "Armbar", TechniquePosition = TechniquePosition.ClosedGuard },
                new Technique { TechniqueName = "Triangle", TechniquePosition = TechniquePosition.ClosedGuard });
            await context.SaveChangesAsync();
            var service = new TechniqueService(new Repository<Technique>(context));

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}