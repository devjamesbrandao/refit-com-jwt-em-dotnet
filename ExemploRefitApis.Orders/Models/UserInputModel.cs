namespace web_teste.Models
{
    public class UserInputModel
    {
        public UserInputModel(){ }
        
        public UserInputModel(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; set; }
        public string Password { get; set; }
    }
}