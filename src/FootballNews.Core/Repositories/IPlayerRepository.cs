using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAll();
        Task<IEnumerable<Player>> GetAllFiltered(string filter);
        Task<Player> GetById(Guid id);
        Task Create(Player player);
        Task Update(Player player);
        Task Delete(Player player);
        Task<IEnumerable<Player>> GetByTeam(string teamId);
    }
}