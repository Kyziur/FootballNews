using System;
using System.Threading.Tasks;
using FootballNews.Core.Domain;

namespace FootballNews.Core.Repositories
{
    public interface ICommentRepository
    {
        public Task<Comment> GetById(Guid id);
        public Task Delete(Comment comment);
        public Task Update(Comment comment);
    }
}