using Moq;
using Sprint4.Models;
using Sprint4.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Sprint4.Tests.Services
{
    public class RecommendationServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly RecommendationService _recommendationService;

        public RecommendationServiceTests()
        {
            // Configurando o mock do repositório de produtos
            _productRepositoryMock = new Mock<IProductRepository>();
            _recommendationService = new RecommendationService(_productRepositoryMock.Object);
        }

        [Fact]
        public void RecommendProducts_ValidUserId_ReturnsRecommendations()
        {
            // Arrange
            var userId = "user123";
            var mockProducts = new List<Product>
            {
                new Product { ProductId = "1", Name = "Product 1", Category = "Electronics" },
                new Product { ProductId = "2", Name = "Product 2", Category = "Books" }
            };

            // Setup para simular a recuperação de produtos
            _productRepositoryMock.Setup(repo => repo.GetProductsByUserId(userId)).Returns(mockProducts);

            // Act
            var recommendations = _recommendationService.RecommendProducts(userId).ToList();

            // Assert
            Assert.NotNull(recommendations);
            Assert.Equal(2, recommendations.Count);
            Assert.Contains(recommendations, p => p.ProductId == "1");
            Assert.Contains(recommendations, p => p.ProductId == "2");
        }

        [Fact]
        public void RecommendProducts_NoProducts_ReturnsEmptyList()
        {
            // Arrange
            var userId = "user123";

            // Setup para simular a recuperação de produtos
            _productRepositoryMock.Setup(repo => repo.GetProductsByUserId(userId)).Returns(new List<Product>());

            // Act
            var recommendations = _recommendationService.RecommendProducts(userId).ToList();

            // Assert
            Assert.Empty(recommendations);
        }
    }
}
