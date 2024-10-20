using System.ComponentModel.DataAnnotations;

namespace AbuOdeh_Electromechanical.Areas.Admin.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Username is required!")]
        [Display(Name = "User Name")]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
