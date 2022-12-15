using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KontaktAPI.Model
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options)
         : base(options)
        {

        }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Contact>().HasData(
               new Contact()
               {
                   Id = 1,
                   Name = "Jan",
                   Surname = "Kowalski",
                   Email = "j.kowalski@gmail.com",
                   Password = "123456",
                   Categories = Category.Private,
                   SubCategory = "",
                   Phone = 123456789,
                   DateOfBirth = DateTime.Now
               },
               new Contact()
               {
                   Id = 2,
                   Name = "Stefan",
                   Surname = "Nowak",
                   Email = "s.Nowak@gmail.com",
                   Password = "123456",
                   Categories = Category.Business,
                   SubCategory = "Szef",
                   Phone = 123456782,
                   DateOfBirth = DateTime.Now
               },
                new Contact()
                {
                    Id = 3,
                    Name = "Janina",
                    Surname = "Kowalska",
                    Email = "j.Kowalska@gmail.com",
                    Password = "123456",
                    Categories = Category.Business,
                    SubCategory = "Klient",
                    Phone = 123456783,
                    DateOfBirth = DateTime.Now
                },
                 new Contact()
                 {
                     Id = 4,
                     Name = "Marek",
                     Surname = "Kowalski",
                     Email = "m.kowalski@gmail.com",
                     Password = "123456",
                     Categories = Category.Private,
                     SubCategory = "",
                     Phone = 123456784,
                     DateOfBirth = DateTime.Now
                 },
                  new Contact()
                  {
                      Id = 5,
                      Name = "Agnieszka",
                      Surname = "Kowalska",
                      Email = "a.Kowalska@gmail.com",
                      Password = "123456",
                      Categories = Category.Private,
                      SubCategory = "",
                      Phone = 123456785,
                      DateOfBirth = DateTime.Now
                  });
           modelBuilder.Entity<Role>().HasData(
           new Role()
           {
               Id = 1,
               RoleName = "User"

           });
        }
    }
}
