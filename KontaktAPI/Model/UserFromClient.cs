using System.ComponentModel.DataAnnotations;

namespace KontaktAPI.Model
{
    public class UserFromClient
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
    }
}
