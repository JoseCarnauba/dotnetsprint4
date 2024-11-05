using Sprint4.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sprint4.Services
{
    // Classe que representa a recomendação de um produto
    public class ProductRecommendation
    {
        public string ProductId { get; set; }
        public float Rating { get; set; }
    }

    // Modelo para armazenar dados relacionados a recomendações
    public class RecommendationModel
    {
        public string UserId { get; set; }
        public List<ProductRecommendation> Recommendations { get; set; } = new List<ProductRecommendation>();
    }

    // Serviço responsável pela lógica de recomendação
    public class RecommendationService
    {
        private readonly ApplicationDbContext _context;

        public RecommendationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ProductRecommendation> RecommendProducts(string userId)
        {
            // Implementação simples da lógica de recomendação
            // Aqui, você pode usar ML.NET ou lógica simples de exemplo

            // Exemplo de produtos fictícios para a recomendação
            var products = _context.Products.ToList();

            // Simulação de recomendações, onde atribuímos uma classificação aleatória para cada produto
            var random = new System.Random();
            var recommendations = products.Select(p => new ProductRecommendation
            {
                ProductId = p.Id.ToString(),
                Rating = (float)(random.NextDouble() * 5) // Avaliação aleatória entre 0 e 5
            })
            .OrderByDescending(r => r.Rating) // Ordena por classificação
            .Take(5) // Retorna os 5 melhores produtos
            .ToList();

            return recommendations;
        }
    }
}
