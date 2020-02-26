using System.Diagnostics;
using System.Threading.Tasks;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FootballNews.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleRepository _articleRepository;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _articleRepository.GetAll();
            return View(articles);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}