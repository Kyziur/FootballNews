using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAll();
        Task<IEnumerable<Game>> GetAllFiltered(string filter);
        Task<Game> GetById(Guid id);
        Task Create(Game game);
        Task Update(Game game);
        Task Delete(Game game);
        Task<IEnumerable<Game>> GetUpcoming();
    }
}