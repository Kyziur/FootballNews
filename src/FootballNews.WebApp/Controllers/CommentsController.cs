using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.ViewModels.Article;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly UserManager<User> _userManager;

        public CommentsController(IArticleRepository articleRepository, UserManager<User> userManager)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
        }
        
        [HttpGet("{articleTitle}")]
        public async Task<IActionResult> Get(string articleTitle)
        {
            var article = await _articleRepository.GetByTitle(articleTitle);
            if (article is null)
            {
                return NotFound("Article with this title has not been found.");
            }
            
            var comments = article.Comments;
            var model = comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Text = x.Text,
                ParentId = x.ParentComment?.Id,
                CreatedDate = x.CreatedAt,
                FullName = x.Author.UserName
            });
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Missing data in body.");
            }

            var article = await _articleRepository.GetByTitle(model.ArticleTitle);
            if (article is null)
            {
                return NotFound("Article with this title has not been found.");
            }
            
            var user = await _userManager.GetUserAsync(User);
            var comment = new Comment(new Guid(), user, model.Text);
            if (model.ParentId.HasValue)
            {
                article.AddComment(comment, model.ParentId.Value);
            }
            else
            {
                article.AddComment(comment);
            }

            await _articleRepository.Update(article);
            model.Id = comment.Id;
            return Ok(model);
        }
    }
}