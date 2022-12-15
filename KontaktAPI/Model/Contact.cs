using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KontaktAPI.Model
{
    public enum Category
    {
        Business,
        Private,
        Other
    }
    public class Contact
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Surname { get; set; }
        [EmailAddress(ErrorMessage = "Wpisz email poprawnie")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Category Categories { get; set; }
        public string SubCategory { get; set; }
        public int Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
