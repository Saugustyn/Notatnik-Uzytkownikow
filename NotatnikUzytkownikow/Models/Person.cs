using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name ="Nazwisko")]
        public string LastName { get; set; }

        [Required]
        [CurrentDate(ErrorMessage = "Data nie może być z przyszłości!")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data urodzenia")]
        public DateTime DateOfBirth { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Required]
        [Display(Name = "Płeć")]
        public string Gender { get; set; }

        [StringLength(150)]
        [Display(Name = "Stanowisko")]
        public string? Position { get; set; }

        [RegularExpression("^([0-9]{9})$")]
        [Display(Name = "Numer telefonu")]
        public string? PhoneNumber { get; set; }

        [Range(50, 250)]
        [Display(Name = "Wzrost")]
        public int? Height { get; set; }

        [Range(20, 50)]
        [Display(Name = "Rozmiar buta")]
        public int? ShoeSize { get; set; }

    }
}
