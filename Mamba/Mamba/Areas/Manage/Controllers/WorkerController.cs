using Mamba.Areas.Manage.ViewModels;
using Mamba.Areas.Manage.ViewModels.Workers;
using Mamba.DAl;
using Mamba.Models;
using Mamba.Utilities.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Mamba.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class WorkerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public WorkerController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index(int page=1,int take=4)
        {
            int count = await _context.Workers.CountAsync();
            List<Worker> workers = await _context.Workers.Skip((page - 1) * take).Take(take).ToListAsync();
            PaginationVM<Worker> pagination = new PaginationVM<Worker>
            {
                TotalPage = Math.Ceiling((double)count / take),
                CurrentPage = page,
                items = workers

            };
            
            return View(pagination);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkerVM vM)
        {
            if (!ModelState.IsValid) return View();
            if (!vM.Photo.CheckFile("image/"))
            {
                ModelState.AddModelError("Photo","file tipi uygun deyil");
                return View(vM);
            }
            if (vM.Photo.CheckSize(5))
            {
                ModelState.AddModelError("Photo", "file olcusu uygun deyil");
                return View(vM);
            }
            string filename = await vM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");

            Worker worker = new Worker
            {
                Name = vM.Name,
                Position = vM.Position,
                Image=filename
            };
           
            await _context.Workers.AddAsync(worker);
            await _context.SaveChangesAsync();
             return RedirectToAction(nameof(Index));

            

            
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            Worker worker =  _context.Workers.FirstOrDefault(x => x.Id == id);
            if (worker == null) return NotFound();
            UpdateWorkerVM vm = new UpdateWorkerVM
            {
                Position = worker.Position,
                Name = worker.Name,
                Image = worker.Image,

            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,UpdateWorkerVM workerVM)
        {
            if (id <= 0) return BadRequest();
            Worker existed = _context.Workers.FirstOrDefault(x => x.Id == id);
            if (existed == null) return NotFound();
            if (workerVM.Photo is not null)
            {
                if (!workerVM.Photo.CheckFile("image/"))
                {
                    ModelState.AddModelError("Photo", "file tipi uygun deyil");
                    return View(workerVM);
                }
                if (workerVM.Photo.CheckSize(5))
                {
                    ModelState.AddModelError("Photo", "file olcusu uygun deyil");
                    return View(workerVM);
                }
                string filename = await workerVM.Photo.CreateFileAsync(_env.WebRootPath, "assets", "img");
                existed.Image.DeleteFile(filename);
                existed.Image = filename;
            }
            existed.Position= workerVM.Position;
            existed.Name= workerVM.Name;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Worker existed = _context.Workers.FirstOrDefault(x => x.Id == id);
            if (existed == null) return NotFound();
            existed.Image.DeleteFile(_env.WebRootPath,"assets","img");
            _context.Workers.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
