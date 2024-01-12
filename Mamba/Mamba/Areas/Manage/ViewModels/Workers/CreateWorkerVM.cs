using Microsoft.Build.Framework;

namespace Mamba.Areas.Manage.ViewModels.Workers
{
    public class CreateWorkerVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Position { get; set; }
        public IFormFile Photo { get; set; }
    }
}
