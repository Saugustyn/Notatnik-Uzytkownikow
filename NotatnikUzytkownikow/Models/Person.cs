using System.ComponentModel.DataAnnotations;

namespace NotatnikUzytkownikow.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }
        [Required]
        [Range(typeof(DateTime), "1/1/1900", "DateTime.Now")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [RegularExpression("^(Male|Female)$")]
        public string Gender { get; set; }
        [StringLength(150)]
        public string? Position { get; set; }
        [RegularExpression("^([0-9]{10}$")]
        public string? PhoneNumber { get; set; }
        [Range(50, 250)]
        public int? Height { get; set; }
        [Range(20, 50)]
        public int? ShoeSize { get; set; }

    }
}
