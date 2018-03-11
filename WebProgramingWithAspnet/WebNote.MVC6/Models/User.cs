using System.ComponentModel.DataAnnotations;

namespace WebNote.MVC6.Models
{
    public class User
    {
        /// <summary>
        /// User Unique Number
        /// </summary>
        [Key]   // primary key
        public int No { get; set; }

        [Required(ErrorMessage ="Input User Name.")]  // not null
        public string Name { get; set; }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
