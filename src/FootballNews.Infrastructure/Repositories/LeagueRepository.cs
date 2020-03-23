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
    public class LeagueRepository : ILeagueRepository
    {
        private readonly DatabaseContext _context;

        public LeagueRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<League>> GetAll()
        {
            return await _context.Leagues.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<League>> GetAllFiltered(string search)
        {
            return await _context.Leagues.AsNoTracking().Where(x => x.Name.Contains(search)).ToListAsync();
        }

        public Task<League> GetById(Guid id)
        {
            return _context.Leagues.FirstAsync(x => x.Id == id);
        }

        public async Task Create(League league)
        {
            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();
        }

        public async Task Update(League league)
        {
            _context.Leagues.Update(league);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(League league)
        {
            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();
        }
    }
}