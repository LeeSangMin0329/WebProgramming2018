using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebNote.MVC6.Models
{
    public class Note
    {
        /// <summary>
        /// Board Unique Number
        /// </summary>
        [Key]
        public int No { get; set; }

        [Required(ErrorMessage ="Write Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Write Contents")]
        public string Contents { get; set; }

        /// <summary>
        /// Writer Unique Number (foreign key)
        /// </summary>
        [Required]  // Writer
        public int UserNo { get; set; }

        [ForeignKey("UserNo")]
        public virtual User User { get; set; }  // virtual keyword : lazy loading (recommended)
    }
}
