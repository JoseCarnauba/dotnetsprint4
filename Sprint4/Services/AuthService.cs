using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Sprint4.Data;
using Sprint4.Models;
using Sprint4.Services.Interfaces; // Supondo que você tenha uma interface para o serviço
using Microsoft.AspNetCore.Identity;


namespace Sprint4.Services
{
    public class AuthService : IAuthService // Implementando uma interface
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration; // Para acessar configurações (ex: JWT)

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> RegisterAsync(string email, string password)
        {
            // Verificar se o usuário já existe
            if (_context.Users.Any(u => u.Email == email))
                throw new Exception("Usuário já existe.");

            var user = new User
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password) // Hash da senha
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public string Authenticate(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                throw new Exception("Credenciais inválidas.");

            // Geração do token JWT
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Email) }),
                Expires = DateTime.UtcNow.AddDays(7), // Expira em 7 dias
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}