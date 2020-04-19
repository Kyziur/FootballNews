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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly DatabaseContext _context;

        public PlayerRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            return await _context.Players.Include(x => x.Team).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetAllFiltered(string filter)
        {
            return await _context.Players.Include(x => x.Team).Where(x =>
                x.FirstName.Contains(filter) || x.LastName.Contains(filter) || x.Position.Contains(filter) ||
                x.Team.Name.Contains(filter)).ToListAsync();
        }

        public async Task<Player> GetById(Guid id)
        {
            return await _context.Players.Include(x => x.Team).FirstAsync(x => x.Id == id);
        }

        public async Task Create(Player player)
        {
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Player player)
        {
            _context.Players.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Player player)
        {
            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Player>> GetByTeam(string teamId)
        {
            var id = Guid.Parse(teamId);
            return await _context.Players.Include(x => x.Team)
                .Where(x => x.Team.Id == id).ToListAsync();
        }
    }
}