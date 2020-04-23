using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.Infrastructure.Repositories;
using FootballNews.WebApp.Extensions;
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
        private readonly ICommentRepository _commentRepository;

        public CommentsController(IArticleRepository articleRepository, UserManager<User> userManager, ICommentRepository commentRepository)
        {
            _articleRepository = articleRepository;
            _userManager = userManager;
            _commentRepository = commentRepository;
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
                FullName = x.Author.UserName,
                CreatedByCurrentUser = x.Author.UserName == User.Identity.Name,
                // CreatedByAdmin = _userManager.IsInRoleAsync(x.Author, Role.Admin).Result,
                CurrentUserIsAdmin = User.IsInRole(Role.Admin),
                UpdatedDate = x.UpdatedAt,
            }).ToList();
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

        [HttpPut]
        public async Task<IActionResult> Put(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Missing data in body.");
            }

            if (!model.Id.HasValue)
            {
                return NotFound();
            }

            var comment = await _commentRepository.GetById(model.Id.Value);
            comment.SetText(model.Text);
            await _commentRepository.Update(comment);
            model.UpdatedDate = comment.UpdatedAt;
            model.Text = comment.Text;
            model.CreatedDate = comment.CreatedAt;
            return Ok(model);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var comment = await _commentRepository.GetById(Guid.Parse(id));
            if (comment is null)
            {
                return NotFound();
            }

            await _commentRepository.Delete(comment);
            return Ok();
        }
    }
    
    
}