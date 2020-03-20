using System;
using System.Collections.Generic;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Manage
{
    public class IndexViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}