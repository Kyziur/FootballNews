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
    public class GameRepository : IGameRepository
    {
        private readonly DatabaseContext _context;

        public GameRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            return await _context.Games
                .Include(x => x.HomeTeam).ThenInclude(x => x.Players)
                .Include(x => x.AwayTeam).ThenInclude(x => x.Players)
                .Include(x => x.Goals).ThenInclude(x => x.Team)
                .Include(x => x.Goals).ThenInclude(x => x.Shooter).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetAllFiltered(string filter)
        {
            return await _context.Games
                .Include(x => x.HomeTeam).ThenInclude(x => x.Players)
                .Include(x => x.AwayTeam).ThenInclude(x => x.Players)
                .Include(x => x.Goals).ThenInclude(x => x.Team)
                .Include(x => x.Goals).ThenInclude(x => x.Shooter)
                .Where(x => x.AwayTeam.Name.Contains(filter) || x.HomeTeam.Name.Contains(filter)).AsNoTracking()
                .ToListAsync();
        }

        public async Task<Game> GetById(Guid id)
        {
            return await _context.Games
                .Include(x => x.HomeTeam).ThenInclude(x => x.Players)
                .Include(x => x.AwayTeam).ThenInclude(x => x.Players)
                .Include(x => x.Goals).ThenInclude(x => x.Team)
                .Include(x => x.Goals).ThenInclude(x => x.Shooter).FirstAsync(x => x.Id == id);
        }

        public async Task Create(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Game game)
        {
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Game game)
        {
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetUpcoming()
        {
            return await _context.Games.Include(x => x.AwayTeam).Include(x => x.HomeTeam)
                .Where(x => x.Date >= DateTime.Today).ToListAsync();
        }
    }
}