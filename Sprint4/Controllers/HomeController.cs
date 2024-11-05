using Microsoft.AspNetCore.Mvc;

namespace Sprint4.Controllers
{
    public class HomeController : Controller // Herda de Controller para utilizar funcionalidades MVC
    {
        // Ação para retornar a página inicial
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Retorna a visualização correspondente à ação Index
        }

        // Ação para retornar dados em formato JSON
        [HttpGet("api/data")]
        public IActionResult GetData()
        {
            var data = new
            {
                Message = "Welcome to the API!",
                Timestamp = DateTime.UtcNow
            };
            return Ok(data); // Retorna um resultado 200 OK com os dados
        }
    }
}
