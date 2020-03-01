using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FootballNews.WebApp.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ILogger<ArticlesController> _logger;
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(ILogger<ArticlesController> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleRepository.GetAll();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string title)
        {
            return View();
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