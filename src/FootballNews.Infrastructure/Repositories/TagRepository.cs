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
    public class TagRepository : ITagRepository
    {
        private readonly DatabaseContext _context;

        public TagRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAll()
        {
            return await _context.Tags.Include(x => x.TagArticles).ThenInclude(x => x.Article).ToListAsync();
        }

        public async Task Create(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<Tag> GetById(Guid id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<IEnumerable<Tag>> GetByIds(IEnumerable<Guid> ids)
        {
            return await _context.Tags.Where(x => ids.Any(y => y == x.Id)).ToListAsync();
        }
    }
}