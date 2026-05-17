namespace Catalogo.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
