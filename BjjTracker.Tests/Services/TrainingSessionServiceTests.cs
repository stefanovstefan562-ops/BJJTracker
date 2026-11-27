using BjjTracker.Models;
using BjjTracker.Models.Enums;
using BjjTracker.Repositories;
using BjjTracker.Services;
using Moq;
using Xunit;

namespace BjjTracker.Tests
{
    public class TrainingSessionServiceTests
    {
        [Fact]
        public async Task GetByIdAsync_WhenTrainingSessionExists_ReturnsTrainingSession()
        {
            // Arrange
            var trainingSession = new TrainingSession
            {
                Id = 1,
                DateOfTheTraining = new DateOnly(2027, 10, 10),
                TrainingType = TypeOfTheTraining.GI
            };
            var mockRepository = new Mock<IRepository<TrainingSession>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trainingSession);
            var service = new TrainingSessionService(mockRepository.Object);
            // Act
            var result = await service.GetByIdAsync(1);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }
        [Fact]
        public async Task DeleteAsync_WhenTrainingSessionExists_DeletesAndSaves()
        {
            // Arrange
            var trainingSession = new TrainingSession
            {
                Id = 1,
                DateOfTheTraining = new DateOnly(2027, 10, 10),
                TrainingType = TypeOfTheTraining.GI
            };
            var mockRepository = new Mock<IRepository<TrainingSession>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(trainingSession);
            var service = new TrainingSessionService(mockRepository.Object);
            // Act
            await service.DeleteAsync(1);
            // Assert
            mockRepository.Verify(r => r.Delete(trainingSession), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_WhenTrainingSessionMissing_DoesNothing()
        {
            // Arrange
            var mockRepository = new Mock<IRepository<TrainingSession>>();
            mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((TrainingSession?)null);
            var service = new TrainingSessionService(mockRepository.Object);
            // Act
            await service.DeleteAsync(1);
            // Assert
            mockRepository.Verify(r => r.Delete(It.IsAny<TrainingSession>()), Times.Never);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Never);
        }
        [Fact]
        public async Task CreateAsync_AddsTrainingSessionAndSaves()
        {
            // Arrange
            var trainingSession = new TrainingSession
            {
                Id = 1,
                DateOfTheTraining = new DateOnly(2027, 10, 10),
                TrainingType = TypeOfTheTraining.GI
            };
            var mockRepository = new Mock<IRepository<TrainingSession>>();
            var service = new TrainingSessionService(mockRepository.Object);
            // Act
            await service.CreateAsync(trainingSession);
            // Assert
            mockRepository.Verify(r => r.AddAsync(trainingSession), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_UpdatesTrainingSessionAndSaves()
        {
            // Arrange
            var trainingSession = new TrainingSession
            {
                Id = 1,
                DateOfTheTraining = new DateOnly(2027, 10, 10),
                TrainingType = TypeOfTheTraining.GI
            };
            var mockRepository = new Mock<IRepository<TrainingSession>>();
            var service = new TrainingSessionService(mockRepository.Object);
            // Act
            await service.UpdateAsync(trainingSession);
            // Assert
            mockRepository.Verify(r => r.Update(trainingSession), Times.Once);
            mockRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
        }
    }
}