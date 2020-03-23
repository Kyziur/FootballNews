using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ILeagueRepository
    {
        Task<IEnumerable<League>> GetAll();
        Task<IEnumerable<League>> GetAllFiltered(string search);
        Task<League> GetById(Guid id);
        Task Create(League league);
        Task Update(League league);
        Task Delete(League league);
    }
}