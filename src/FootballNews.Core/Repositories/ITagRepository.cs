using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll();
    }
}