using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballNews.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DatabaseContext _context;

        public TeamRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAll()
        {
            return await _context.Teams
                .Include(x => x.Players)
                .Include(x => x.League)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Team>> GetAllFiltered(string filter)
        {
            return await _context.Teams
                .Include(x => x.Players)
                .Include(x => x.League).Where(x => x.Name.Contains(filter))
                .AsNoTracking().ToListAsync();
        }

        public async Task<Team> GetById(Guid id)
        {
            return await _context.Teams
                .Include(x => x.Players)
                .Include(x => x.League)
                .FirstAsync(x => x.Id == id);
        }

        public async Task Create(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Team team)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Team>> GetByLeagueName(string league)
        {
            return await _context.Teams
                .Include(x => x.League)
                .Include(x => x.HomeMatches).ThenInclude(x => x.Goals).ThenInclude(x => x.Team)
                .Include(x => x.AwayMatches).ThenInclude(x => x.Goals).ThenInclude(x => x.Team)
                .Where(x => x.League.Name == league)
                .OrderByDescending(x => x.Points).ToListAsync();
        }
    }
}