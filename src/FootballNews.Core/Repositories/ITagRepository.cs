using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ITagRepository : IRepository
    {
        Task<IEnumerable<Tag>> GetAll();
    }
}