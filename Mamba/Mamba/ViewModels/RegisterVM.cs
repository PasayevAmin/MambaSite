using Mamba.Utilities.Enums;
using System.ComponentModel.DataAnnotations;

namespace Mamba.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage ="You can't empty this value")]
        [MinLength(3,ErrorMessage ="your name must be bigger than 3 symbol")]
        [MaxLength(25, ErrorMessage = "your name must be less than 25 symbol")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You can't empty this value")]
        [MinLength(4, ErrorMessage = "your name must be bigger than 4 symbol")]
        [MaxLength(50, ErrorMessage = "your name must be less than 50 symbol")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "You can't empty this value")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "You can't empty this value")]
        [MinLength(8,ErrorMessage ="your password must be bigger than 8 symbol")]

        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "You can't empty this value")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]

        public string CinfirmPassword { get; set; }
        [Required(ErrorMessage = "You can't empty this value")]

        public Gender Gender { get; set; }
        [Required(ErrorMessage = "You can't empty this value")]
        [MinLength(4, ErrorMessage = "your name must be bigger than 4 symbol")]
        [MaxLength(50, ErrorMessage = "your name must be less than 50 symbol")]
        public string UserName { get; set; }

    }
}
