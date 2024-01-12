using Mamba.DAl;
using Mamba.Models;
using Mamba.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Mamba.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Worker> workers = await _context.Workers.ToListAsync();
            HomeVM vm = new HomeVM
            {
                Workers = workers,
            };
            return View(vm);
        }

     
    }
}