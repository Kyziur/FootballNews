using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
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
        private readonly ILeagueRepository _leagueRepository;
        private readonly UserManager<User> _userManager;

        public ArticlesController(IArticleRepository articleRepository, ITagRepository tagRepository, ILeagueRepository leagueRepository, UserManager<User> userManager)
        {
            _articleRepository = articleRepository;
            _tagRepository = tagRepository;
            _leagueRepository = leagueRepository;
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var articles = await _articleRepository.GetAllFiltered(search);
            var onePageArticles = await articles.ToPagedListAsync(page, 10);
            var model = onePageArticles.Select(x => new IndexArticleModel
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                League = x.League?.Name ?? "",
                Author = x.Author?.UserName ?? "",
                ImageAsBytes = x.Image,
                Created = x.CreatedAt.ToShortDateString(),
                Tags = x.ArticlesTags.Select(t => t.Tag.Name).ToList()
            });
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var tags = await _tagRepository.GetAll();
            var leagues = await _leagueRepository.GetAll();
            var model = new ArticleModel
            {
                Tags = tags.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList(),
                Leagues = leagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                var allTags = await _tagRepository.GetAll();
                var allLeagues = await _leagueRepository.GetAll();
                model.Tags = allTags.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                model.Leagues = allLeagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
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
            
            var league = await _leagueRepository.GetById(Guid.Parse(model.SelectedLeagueId));
            article.AssignToLeague(league);
            
            var user = await _userManager.GetUserAsync(User);
            article.SetAuthor(user);
            
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
            var leagues = await _leagueRepository.GetAll();
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
                }).ToList(),
                SelectedTagsIds = article.Tags.Select(x => x.Id.ToString()).ToList(),
                SelectedLeagueId = article.League.Id.ToString(),
                Leagues = leagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleModel model)
        {
            if (!ModelState.IsValid)
            {
                var allTags = await _tagRepository.GetAll();
                var allLeagues = await _leagueRepository.GetAll();
                model.Tags = allTags.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
                model.Leagues = allLeagues.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
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
            var league = await _leagueRepository.GetById(Guid.Parse(model.SelectedLeagueId));
            article.AssignToLeague(league);
            
            await _articleRepository.Update(article);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var article = await _articleRepository.GetById(id);
                await _articleRepository.Delete(article);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not delete article with id: {id}");
                return BadRequest();
            }

            return Ok();
        }
        
    }
}