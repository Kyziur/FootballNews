using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> GetAllFiltered(string searchString);
        IQueryable<Article> GetAllAsQueryable();
        Task<Article> GetById(Guid id);
        Task<Article> GetByTitle(string title);
        Task Create(Article article);
        Task Update(Article article);
        Task Delete(Article article);
        Task<bool> IsThereArticleOrCommentMadeByAuthor(string username);

    }
}