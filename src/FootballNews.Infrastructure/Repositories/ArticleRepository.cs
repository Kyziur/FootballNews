using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballNews.Core.Domain;
using FootballNews.Core.Repositories;

namespace FootballNews.Infrastructure.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public async Task<IEnumerable<Article>> GetAll()
        {
            var articles = Enumerable.Range(1, 10).Select(x => new Article($"Title {x}",
                $"Content {x} Lorem ipsum dolor sit amet, consectetur adipisicing elit. Amet at ea enim error fugiat impedit libero magni, nam necessitatibus nemo nulla numquam odio quam quibusdam quidem, reiciendis voluptas. Repellat, repellendus!</div><div>Aliquid, atque blanditiis consectetur consequuntur culpa cumque dolore fugit illo ipsam iste itaque minus nam necessitatibus nostrum obcaecati odit omnis porro quasi quis saepe sit soluta veritatis voluptatem voluptatibus voluptatum.</div><div>Accusamus ad aliquid amet blanditiis dolore, ea, esse eveniet hic ipsum iste laboriosam laborum laudantium odio optio, praesentium quidem sequi sint soluta. Earum nam nemo, nisi provident repellat sunt tempore!</div><div>Accusantium ad asperiores aut, consequuntur cumque cupiditate deserunt dicta dignissimos eum excepturi ipsum iste itaque magnam maiores minus nesciunt nobis numquam obcaecati placeat provident quas quisquam quos rerum temporibus veritatis.</div><div>Consequuntur doloremque ea earum facere, illum iure magni maxime modi, non quas quis quos repellendus sit tempore ut? Cupiditate enim magni odit, possimus quos ratione sint sit tempore totam voluptas!"));
            return await Task.FromResult(articles);
        }

        public Task<Article> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Create(Article article)
        {
            throw new NotImplementedException();
        }

        public Task Update(Article article)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Article article)
        {
            throw new NotImplementedException();
        }
    }
}