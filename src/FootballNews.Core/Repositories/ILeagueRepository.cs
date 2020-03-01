using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ILeagueRepository
    {
        Task<IEnumerable<League>> GetAll();
        Task<League> GetById(Guid id);
        Task Create(League article);
        Task Update(League article);
        Task Delete(League article);
    }
}