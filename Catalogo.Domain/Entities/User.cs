using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogo.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public User(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }
    }
}
