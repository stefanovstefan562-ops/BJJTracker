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
    }
}