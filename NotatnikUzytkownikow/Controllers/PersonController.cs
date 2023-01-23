using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotatnikUzytkownikow.Data;
using NotatnikUzytkownikow.Models;
using OfficeOpenXml;
using System.ComponentModel;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using LicenseContext = OfficeOpenXml.LicenseContext;

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
        [AutoValidateAntiforgeryToken]
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
        }
        public IActionResult GenerateReport()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();

            // Add a new worksheet to the Excel package
            var worksheet = excel.Workbook.Worksheets.Add("Użytkownicy");

            // Add headers to the worksheet
            worksheet.Cells[1, 1].Value = "Tytuł";
            worksheet.Cells[1, 2].Value = "Imię";
            worksheet.Cells[1, 3].Value = "Nazwisko";
            worksheet.Cells[1, 4].Value = "Data urodzenia";
            worksheet.Cells[1, 5].Value = "Płeć";
            worksheet.Cells[1, 6].Value = "Wiek";

            // Use Entity Framework to retrieve data from the database
            var users = _db.Persons.ToList();

            // Loop through the users and add the data to the worksheet
            for (int i = 0; i < users.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = users[i].Gender == "Female" ? "Pani" : "Pan";
                worksheet.Cells[i + 2, 2].Value = users[i].FirstName;
                worksheet.Cells[i + 2, 3].Value = users[i].LastName;
                worksheet.Cells[i + 2, 4].Value = users[i].DateOfBirth.ToString();
                worksheet.Cells[i + 2, 5].Value = users[i].Gender;
                worksheet.Cells[i + 2, 6].Value = DateTime.Now.Year - users[i].DateOfBirth.Year;
            }

            string fileName = string.Format("{0}.xlsx", DateTime.Now.ToString("yyyy.MM.dd:HH-mm-ss"));

            // Return the Excel file to the client
            return File(excel.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}