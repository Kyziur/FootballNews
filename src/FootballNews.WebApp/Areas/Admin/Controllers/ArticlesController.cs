using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Article;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticlesController : BaseController
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ITagRepository _tagRepository;

        public ArticlesController(IArticleRepository articleRepository, ITagRepository tagRepository)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var articles = await _articleRepository.GetAll();
            var onePageArticles = await articles.ToPagedListAsync(page, 5);
            var model = onePageArticles.Select(x => new IndexArticleModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                ImageAsBytes = x.Image,
                Tags = x.ArticlesTags.Select(t => t.Tag.Name).ToList()
            });
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var tags = await _tagRepository.GetAll();
            var model = new ArticleModel
            {
                Tags = tags.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var article = new Article(Guid.NewGuid(),model.Title, model.Content);
            
            if (model.Image != null)
            {
                await using var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                article.SetImage(stream.ToArray(), Path.GetRandomFileName());
            }

            var tags = await GetTagsFromModel(model);
            article.SetTags(tags);
            
            await _articleRepository.Create(article);
            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<Tag>> GetTagsFromModel(ArticleModel model)
        {
            if (model.SelectedTagsIds is null)
                return null;

            var selectedTagsIds = model.SelectedTagsIds.Select(Guid.Parse);
            var tags = await _tagRepository.GetByIds(selectedTagsIds);
            return tags;
        }
        
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var tags = await _tagRepository.GetAll();
            var article = await _articleRepository.GetById(id);
            var model = new ArticleModel
            {
                Id = article.Id,
                Title = article.Title,
                Content = article.Content,
                ImageAsBytes = article.Image, 
                Tags = tags.Select(t => new SelectListItem
                {
                    Text = t.Name,
                    Value = t.Id.ToString(),
                    Selected = article.ArticlesTags.Any(x => x.TagId == t.Id)
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var article = await _articleRepository.GetById(model.Id);

            if (model.Image != null)
            {
                await using var stream = new MemoryStream();
                await model.Image.CopyToAsync(stream);
                article.SetImage(stream.ToArray(), Path.GetRandomFileName());
            }
            
            article.SetTitle(model.Title);
            article.SetContent(model.Content);
            
            var tags = await GetTagsFromModel(model);
            article.SetTags(tags);
            
            await _articleRepository.Update(article);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var article = await _articleRepository.GetById(id);
            if (article is null)
            {
                return BadRequest();
            }

            await _articleRepository.Delete(article);
            return Ok();
        }
        
    }
}