using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;
using FootballNews.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FootballNews.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DatabaseContext _context;

        public ArticleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllFiltered(string searchString)
        {
            return await _context.Articles
                .Include(x => x.ArticlesTags)
                .ThenInclude(x => x.Tag).Where(x =>
                    x.Title.Contains(searchString) || x.Content.Contains(searchString))
                .ToListAsync();
        }

        public IQueryable<Article> GetAllAsQueryable()
        {
            return _context.Articles
                .Include(x => x.ArticlesTags)
                .ThenInclude(x => x.Tag);
        }

        public async Task<Article> GetById(Guid id)
        {
            return await _context.Articles.Include(x => x.ArticlesTags).FirstAsync(x => x.Id == id);
        }

        public Task<Article> GetByTitle(string title)
        {
            return _context.Articles.Include(x => x.ArticlesTags).ThenInclude(x => x.Tag)
                .Include(x => x.Comments)
                .ThenInclude(x => x.Author)
                .Include(x => x.Comments)
                .ThenInclude(x => x.ParentComment)
                .ThenInclude(x => x.Author)
                .FirstAsync(x => x.Title == title);
        }


        public async Task Create(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Article article)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }
}