using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FootballNews.Core.Domain
{
    public class User : IdentityUser<Guid>
    {
        public IEnumerable<Article> CreatedArticles { get; private set; }
    }
}