using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Tag;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class TagsController : BaseController
    {
        private readonly ITagRepository _tagRepository;

        public TagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var tags = await _tagRepository.GetAllFiltered(search);
            var onePageTags = await tags.ToPagedListAsync(page, 10);
            var model = onePageTags.Select(x => new TagModel
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tag = new Tag(Guid.NewGuid(), model.Name);
            await _tagRepository.Create(tag);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var tag = await _tagRepository.GetById(Guid.Parse(id));

            var model = new TagModel
            {
                Id = tag.Id.ToString(),
                Name = tag.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TagModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tag = await _tagRepository.GetById(Guid.Parse(model.Id));
            tag.SetName(model.Name);

            await _tagRepository.Update(tag);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _tagRepository.Delete(Guid.Parse(id));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not delete the tag with id: {id}");
                return BadRequest();
            }

            return Ok();
        }
    }
}