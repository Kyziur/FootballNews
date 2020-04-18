using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAll();
        Task<IEnumerable<Team>> GetAllFiltered(string filter);
        Task<Team> GetById(Guid id);
        Task Create(Team team);
        Task Update(Team team);
        Task Delete(Team team);
        Task<IEnumerable<Team>> GetByLeagueName(string league);
    }
}