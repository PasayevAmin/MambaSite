using System.ComponentModel.DataAnnotations;

namespace Mamba.Areas.Manage.ViewModels.Workers
{
    public class UpdateWorkerVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public string? Image { get; set; }
        public IFormFile Photo { get; set; }
    }
}
