using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotatnikUzytkownikow.Data;
using NotatnikUzytkownikow.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        //GET
        public IActionResult Upsert(int? id)
        {
            Person person = new();

            if (id == null || id == 0)
            {
                return View(person);
            }
            else
            {
                person = _db.Persons.Where(x => x.Id == id).FirstOrDefault();
                return View(person);
            }
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken] //https://www.devcurry.com/2013/01/what-is-antiforgerytoken-and-why-do-i.html
        public IActionResult Upsert(Person obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _db.Persons.Add(obj);
                }
                else
                {
                    _db.Persons.Update(obj);
                }
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
            return RedirectToAction("Index");

        }
    }
}