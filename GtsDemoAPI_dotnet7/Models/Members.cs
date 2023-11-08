using System.ComponentModel.DataAnnotations;

namespace GtsDemoAPI_dotnet7.Models
{
    public class Members
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Email { get; set; }

        [Required]
        //[MaxLength(13)]
        //[MinLength(10)]
        [Phone]
        public string? Phone {  get; set; }

        public int Gender { get; set; }
        /*
        public enum Gender
        { 
            Male,
            Female
        }*/
    }
}
