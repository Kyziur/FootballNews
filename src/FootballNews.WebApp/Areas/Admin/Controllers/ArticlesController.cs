using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class ArticlesController : BaseController
    {
        private readonly IArticleRepository _articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleRepository.GetAll();
            var model = articles.Select(x => new ArticleModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                Image = x.Image
            });
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(ArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var article = new Article(model.Title, model.Content);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return View();
        }

        [HttpPut]
        public async Task<IActionResult> Update(ArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok();
        }
        
    }
}