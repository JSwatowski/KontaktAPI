using System.ComponentModel.DataAnnotations;

namespace KontaktAPI.Model
{
    public class ContactUpdate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }   
        public Category Categories { get; set; }
        public string SubCategory { get; set; }
        public int Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
