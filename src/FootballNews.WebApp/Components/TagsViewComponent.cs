using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Repositories;
using FootballNews.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace FootballNews.WebApp.Components
{
    public class TagsViewComponent : ViewComponent
    {
        private readonly ITagRepository _tagRepository;


        public TagsViewComponent(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var tags = await _tagRepository.GetAll();
            var model = tags.Select(x => new TagModel
            {
                Name = x.Name,
                NumberOfArticles = x.TagArticles.Count()
            });
            
            return View("SideTagList", model);
        }
    }
}