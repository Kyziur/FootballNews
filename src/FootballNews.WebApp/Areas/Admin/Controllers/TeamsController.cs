using System;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.Team;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ITeamRepository _teamRepository;

        public TeamsController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var teams = await _teamRepository.GetAllFiltered(search);
            var onePageTeams = await teams.ToPagedListAsync(page, 10);
            var model = onePageTeams.Select(x => new TeamModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TeamModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TeamModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var league = new Team(Guid.NewGuid(), model.Name);
            await _teamRepository.Create(league);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var team = await _teamRepository.GetById(id);
            var model = new TeamModel
            {
                Id = team.Id,
                Name = team.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(TeamModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var league = await _teamRepository.GetById(model.Id);
            league.SetName(model.Name);
            await _teamRepository.Update(league);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var league = await _teamRepository.GetById(id);
            await _teamRepository.Delete(league);
            return Ok();
        }
    }
}