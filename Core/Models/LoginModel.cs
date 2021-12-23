using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "حقل عنوان البريد الإلكتروني مطلوب")]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "حقل كلمة المرور مطلوب")]
        [MaxLength(50)]
        public string Password { get; set; }

    }
}
