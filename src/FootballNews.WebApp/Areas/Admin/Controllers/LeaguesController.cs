using System;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.WebApp.Areas.Admin.ViewModels.League;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace FootballNews.WebApp.Areas.Admin.Controllers
{
    public class LeaguesController : BaseController
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeaguesController(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            var leagues = await _leagueRepository.GetAllFiltered(search);
            var onePageLeagues = await leagues.ToPagedListAsync(page, 10);
            var model = onePageLeagues.Select(x => new LeagueModel
            {
                Id = x.Id,
                Name = x.Name
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new LeagueModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(LeagueModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var league = new League(Guid.NewGuid(), model.Name);
            await _leagueRepository.Create(league);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var league = await _leagueRepository.GetById(id);
            var model = new LeagueModel {Id = league.Id, Name = league.Name};

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LeagueModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var league = await _leagueRepository.GetById(model.Id);
            league.SetName(model.Name);
            await _leagueRepository.Update(league);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var league = await _leagueRepository.GetById(id);
            await _leagueRepository.Delete(league);
            return Ok();
        }
    }
}