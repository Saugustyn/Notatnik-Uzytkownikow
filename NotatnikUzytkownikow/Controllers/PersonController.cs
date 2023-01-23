using Microsoft.AspNetCore.Mvc;
using NotatnikUzytkownikow.Data;
using NotatnikUzytkownikow.Models;
using System.Diagnostics;

namespace NotatnikUzytkownikow.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PersonController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Person> objPersonList = _db.Persons;
            return View(objPersonList);
        }
    }
}