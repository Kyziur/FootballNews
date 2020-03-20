using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FootballNews.WebApp.Areas.Admin.ViewModels.Manage
{
    public class UpdateViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public IList<SelectListItem> Roles { get; set; }
        public IList<string> SelectedRoles { get; set; }
    }
}