using Mamba.Utilities.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mamba.Models
{
    public class AppUser:IdentityUser
    {
        [Required]
        public Gender Gender { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
