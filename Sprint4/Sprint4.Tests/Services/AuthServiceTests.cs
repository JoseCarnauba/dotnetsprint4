using Moq;
using Sprint4.Models;
using Sprint4.Services;
using System.Threading.Tasks;
using Xunit;

namespace Sprint4.Tests.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            // Configurando o mock do repositório de usuários
            _userRepositoryMock = new Mock<IUserRepository>();
            _authService = new AuthService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Register_UserAlreadyExists_ReturnsFalse()
        {
            // Arrange
            var user = new User { Email = "test@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);

            // Act
            var result = await _authService.Register(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task Register_NewUser_ReturnsTrue()
        {
            // Arrange
            var user = new User { Email = "newuser@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync((User)null);
            _userRepositoryMock.Setup(repo => repo.CreateUserAsync(user)).ReturnsAsync(true);

            // Act
            var result = await _authService.Register(user);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = "hashedPassword" };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.VerifyPasswordAsync(user.PasswordHash, "plainPassword")).ReturnsAsync(true);

            // Act
            var result = await _authService.Login(user.Email, "plainPassword");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task Login_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = "hashedPassword" };
            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.VerifyPasswordAsync(user.PasswordHash, "wrongPassword")).ReturnsAsync(false);

            // Act
            var result = await _authService.Login(user.Email, "wrongPassword");

            // Assert
            Assert.Null(result);
        }
    }
}

