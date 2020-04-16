namespace FootballNews.WebApp.Areas.Admin.ViewModels.Game
{
    public class PlayerJsonModel
    {
        public PlayerJsonModel(string id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
        
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}