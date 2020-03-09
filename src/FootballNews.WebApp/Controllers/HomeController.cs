using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.ViewModels;
using FootballNews.WebApp.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FootballNews.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleRepository _articleRepository;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string tag = "")
        {
            var articles = await _articleRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(tag))
            {
                articles = articles.Where(x => x.Tags.Any(y => y.Name == tag));
            }

            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> Show(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return NotFound("Article with this title has not been found.");
            }

            var article = await _articleRepository.GetByTitle(title);
            var model = new ArticleModel
            {
                Title = article.Title,
                Content = article.Content,
                ImageAsBytes = article.Image
            };

            return View(model);
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