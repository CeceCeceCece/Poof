namespace Application.Models.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
