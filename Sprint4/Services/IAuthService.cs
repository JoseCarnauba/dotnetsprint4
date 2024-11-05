namespace Sprint4.Services
{
    using System.Threading.Tasks;

    public interface IAuthService
    {
        Task<string> Authenticate(UserLoginDto userLoginDto);
    }

// Services/AuthService.cs
using System.Threading.Tasks;

public class AuthService : IAuthService
    {
        public Task<string> Authenticate(UserLoginDto userLoginDto)
        {
            // Lógica de autenticação (ex: verificação de usuário e senha)
            // Retorne um token JWT se a autenticação for bem-sucedida
        }
    }
}
