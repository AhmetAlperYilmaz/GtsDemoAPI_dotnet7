using System.ComponentModel.DataAnnotations;

namespace GtsDemoAPI_dotnet7.Models
{
    public class Users
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Name { get; set; }

        [Required]
        [MaxLength(25)]
        public string? Surname { get; set; }
    }
}
