using KontaktAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KontaktAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly ContactDbContext _dbContext;

        public ContactController(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// akcja zwracająca liste wszystkich kontaktów
        /// </summary>
        /// <returns>kontakty</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAll()
        {
            var contacts = _dbContext.Contacts.ToList();

            return Ok(contacts);
        }
        /// <summary>
        /// akcja zwracająca pojedynczy kontakt po 1
        /// </summary>
        /// <param name="id">id kontaktu </param>
        /// <returns>kontakt </returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult<Contact> Get([FromRoute] int id)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(n=> n.Id == id);

            if(contact == null)
            {
                return NotFound();  
            }

            return Ok(contact);
        }
        /// <summary>
        /// akcja która pobiera od klienta dane aby stworzyć kontakt 
        /// </summary>
        /// <param name="contact">dane do tworzenia kontaktu</param>
        /// <returns>zwraca stworzony kontakt </returns>
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult CreateContact([FromBody] Contact contact)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var uniqeMail = _dbContext.Contacts.FirstOrDefault(n => n.Email == contact.Email);
            if(uniqeMail != null)
            {
                return BadRequest("Taki email już istnieje");
            }

            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
            return Created("/api/Contact/{contact.Id}", null);
        }
        /// <summary>
        /// akcja do usuwania kontaktu
        /// </summary>
        /// <param name="id">id kontaktu do usunięcia</param>
        /// <returns>typ żądania czy jest ok czy nie</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult Delete([FromRoute] int id)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(n => n.Id == id);
            if (contact == null)
            {
                return NoContent();
            }
            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();
            return Ok();
        }
        /// <summary>
        /// akcja do zmiany danych kontaktu
        /// </summary>
        /// <param name="contact">dane do zmiany kontaktu</param>
        /// <param name="id">id kontaktu do zmiany</param>
        /// <returns>typ żądania czy jest ok czy nie </returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public ActionResult Update([FromBody]ContactUpdate contact,[FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contactToUpdate = _dbContext.Contacts.FirstOrDefault(n => n.Id == id);
            
            if (contactToUpdate == null)
            {
                return NoContent();
            }
            contactToUpdate.Name = contact.Name;
            contactToUpdate.Surname = contact.Surname;
            contactToUpdate.Email = contact.Email;
            contactToUpdate.Categories = contact.Categories;
            contactToUpdate.SubCategory = contact.SubCategory;
            contactToUpdate.Phone = contact.Phone;
            contactToUpdate.DateOfBirth = contact.DateOfBirth;
            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
