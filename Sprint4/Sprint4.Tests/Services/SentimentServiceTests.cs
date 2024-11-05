using Moq;
using Sprint4.Services;
using System.Threading.Tasks;
using Xunit;

namespace Sprint4.Tests.Services
{
    public class SentimentServiceTests
    {
        private readonly Mock<ISentimentAnalysisService> _sentimentAnalysisServiceMock;
        private readonly SentimentService _sentimentService;

        public SentimentServiceTests()
        {
            // Configurando o mock do serviço de análise de sentimento
            _sentimentAnalysisServiceMock = new Mock<ISentimentAnalysisService>();
            _sentimentService = new SentimentService(_sentimentAnalysisServiceMock.Object);
        }

        [Fact]
        public async Task AnalyzeSentiment_ValidText_ReturnsPositiveSentiment()
        {
            // Arrange
            var text = "I love this product!";
            var expectedSentiment = "Positive";

            // Setup para simular a análise de sentimento
            _sentimentAnalysisServiceMock.Setup(service => service.AnalyzeAsync(text))
                .ReturnsAsync(expectedSentiment);

            // Act
            var result = await _sentimentService.AnalyzeSentiment(text);

            // Assert
            Assert.Equal(expectedSentiment, result);
        }

        [Fact]
        public async Task AnalyzeSentiment_NegativeText_ReturnsNegativeSentiment()
        {
            // Arrange
            var text = "I hate this product!";
            var expectedSentiment = "Negative";

            // Setup para simular a análise de sentimento
            _sentimentAnalysisServiceMock.Setup(service => service.AnalyzeAsync(text))
                .ReturnsAsync(expectedSentiment);

            // Act
            var result = await _sentimentService.AnalyzeSentiment(text);

            // Assert
            Assert.Equal(expectedSentiment, result);
        }

        [Fact]
        public async Task AnalyzeSentiment_EmptyText_ReturnsNeutralSentiment()
        {
            // Arrange
            var text = "";
            var expectedSentiment = "Neutral";

            // Setup para simular a análise de sentimento
            _sentimentAnalysisServiceMock.Setup(service => service.AnalyzeAsync(text))
                .ReturnsAsync(expectedSentiment);

            // Act
            var result = await _sentimentService.AnalyzeSentiment(text);

            // Assert
            Assert.Equal(expectedSentiment, result);
        }
    }
}
