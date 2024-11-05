using global::Sprint4.Services;

using Xunit;

namespace Sprint4.Sprint4.Tests.Controllers
{
    public class AuthServiceTests
    {
        public object Assert { get; private set; }

        [Fact]
        public void TestAuthenticate_Success()
        {
            // Arrange
            var authService = new AuthService();

            // Act
            var token = authService.Authenticate(new UserLoginDto { Email = "test@example.com", Password = "password" });

            // Assert
            Assert.NotNull(token);
        }
    }
}
