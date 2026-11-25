using BjjTracker.Models;
using BjjTracker.Models.Enums;
using BjjTracker.Repositories;
using BjjTracker.Services;
using Moq;
using Xunit;

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
    }
}