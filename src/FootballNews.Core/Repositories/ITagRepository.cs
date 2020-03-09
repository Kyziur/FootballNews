using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll();
        Task Create(Tag tag);
        Task Update(Tag tag);
        Task Delete(Guid id);
        Task<Tag> GetById(Guid id);
        Task<IEnumerable<Tag>> GetByIds(IEnumerable<Guid> ids);

    }
}