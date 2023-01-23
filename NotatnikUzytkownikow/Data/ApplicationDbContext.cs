using Microsoft.EntityFrameworkCore;
using NotatnikUzytkownikow.Models;

namespace NotatnikUzytkownikow.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
    }
}
