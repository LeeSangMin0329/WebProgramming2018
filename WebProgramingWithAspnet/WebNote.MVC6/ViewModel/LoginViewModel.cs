using System.ComponentModel.DataAnnotations;

namespace WebNote.MVC6.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Write User ID.")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Write User Password.")]
        public string UserPassword { get; set; }
    }
}
