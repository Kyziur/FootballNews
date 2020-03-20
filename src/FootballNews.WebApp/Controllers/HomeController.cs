using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.ViewModels;
using FootballNews.WebApp.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using X.PagedList;

namespace FootballNews.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleRepository _articleRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository, UserManager<User> userManager)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string tag = "", int page = 1)
        {
            var articles = _articleRepository.GetAllAsQueryable();
            
            if (!string.IsNullOrWhiteSpace(tag))
            {
                articles = articles.Where(x => x.ArticlesTags.Any(y => y.Tag.Name == tag));
            }
            
            var articlesOrderedByCreatedDate = articles.ToList().OrderByDescending(x => x.CreatedAt);
            var onePageArticles = await articlesOrderedByCreatedDate.ToPagedListAsync(page, 10);
        

            return View(onePageArticles);
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
            var article = await _articleRepository.GetByTitle(title);
            var model = new ArticleModel
            {
                Title = article.Title,
                Content = article.Content,
                ImageName = article.ImageName,
                ImageAsBytes = article.Image,
                CreatedDate = article.CreatedAt,
                TagsIdsWithNames = article.Tags.Select(x => new Tuple<Guid, string>(x.Id, x.Name)).ToList()
            };
            return View(model);
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